using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using moodle_external_database_integration.Core.Models.Moodle;
using moodle_external_database_integration.Core.Services;
using moodle_external_database_integration.Data;

namespace moodle_external_database_integration.Client.Services;

public class MoodleTransferService : IMoodleTransferService
{
    private readonly MoodleExternalDatabaseIntegrationDbContext _dbContext;
    private readonly ILogger<MoodleTransferService> _logger;
    public MoodleTransferService(MoodleExternalDatabaseIntegrationDbContext dbContext, ILogger<MoodleTransferService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> MapExternalEnrolmentsAsync()
    {
        var dbExternalEnrolments = await _dbContext.ExternalTransferCoursesUsers
                                    .Include(i => i.ExternalTransferCourse).ThenInclude(i => i.MoodleCourse)
                                    .Include(i => i.ExternalTransferUser).ThenInclude(i => i.MoodleUser)
                                    .Where(w => w.ExternalTransferCourse.MoodleCourse != null && w.ExternalTransferUser.MoodleUser != null)
                                    .ToListAsync();

        var enrolmentsToAdd = new List<MoodleEnrolment>();

        foreach (var externalDbEnrolment in dbExternalEnrolments)
        {
            var course = externalDbEnrolment.ExternalTransferCourse.MoodleCourse;
            var user = externalDbEnrolment.ExternalTransferUser.MoodleUser;

            enrolmentsToAdd.Add(new MoodleEnrolment(course, user));
        }

        var existingEnrolments = await _dbContext.MoodleEnrolments.ToListAsync();

        enrolmentsToAdd = enrolmentsToAdd.Where(w => !existingEnrolments.Any(a => a.MoodleCourseId == w.MoodleCourseId && a.MoodleUserId == w.MoodleUserId)).ToList();

        _logger.LogInformation("Adding {count} new Enrolments.", enrolmentsToAdd.Count);

        try
        {
            await _dbContext.MoodleEnrolments.AddRangeAsync(enrolmentsToAdd);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error updating Database for Moodle Enrolments.");

            return false;
        }

        return true;
    }

    public async Task<bool> MapExternalUsersAsync()
    {
        var dbExternalUsers = await _dbContext.ExternalTransferUsers.Include(i => i.MoodleUser).Where(w => w.MoodleUser == null).ToListAsync();
        var usersToAdd = new List<MoodleUser>();

        foreach (var externalDbUser in dbExternalUsers)
        {
            _logger.LogInformation("Creating new User {name}.", externalDbUser.UserName);

            var newMoodleUser = new MoodleUser(externalDbUser);

            // Password should obviously not be static!
            newMoodleUser.Password = "12345";

            usersToAdd.Add(newMoodleUser);
        }

        try
        {
            await _dbContext.MoodleUsers.AddRangeAsync(usersToAdd);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error updating Database for Moodle Users.");

            return false;
        }

        return true;
    }

    public async Task<bool> MapExternalCoursesAsync()
    {
        var dbExternalCourses = await _dbContext.ExternalTransferCourses.Include(i => i.MoodleCourse).Where(w => w.MoodleCourse == null).ToListAsync();
        var coursesToAdd = new List<MoodleCourse>();

        foreach (var externalDbCourse in dbExternalCourses)
        {
            _logger.LogInformation("Creating new Course {name}.", externalDbCourse.FullName);

            var newMoodleCourse = new MoodleCourse(externalDbCourse);

            coursesToAdd.Add(newMoodleCourse);
        }

        try
        {
            await _dbContext.MoodleCourses.AddRangeAsync(coursesToAdd);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Error updating Database for Moodle Courses.");

            return false;
        }

        return true;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Scoped Moodle Transfer Service is working.");

            var isExternalCourseMappingSuccess = await this.MapExternalCoursesAsync();

            _logger.LogInformation("External Courses to Moodle Courses Success: {success}.", isExternalCourseMappingSuccess);

            var isExternalUserMappingSuccess = await this.MapExternalUsersAsync();

            _logger.LogInformation("External Users to Moodle Users Success: {success}.", isExternalUserMappingSuccess);

            var isExternalEnrolmentMappingSuccess = await this.MapExternalEnrolmentsAsync();

            _logger.LogInformation("External Enrolments to Moodle Enrolments Success: {success}.", isExternalEnrolmentMappingSuccess);

            await Task.Delay(10000, stoppingToken);
        }
    }
}