using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using moodle_external_database_integration.Core.DTO.External;
using moodle_external_database_integration.Core.Models.External;
using moodle_external_database_integration.Core.Services;
using moodle_external_database_integration.Data;

namespace moodle_external_database_integration.Client.Services;

public class ExternalTransferService : IExternalTransferService
{
    private readonly MoodleExternalDatabaseIntegrationDbContext _dbContext;
    private readonly ILogger<ExternalTransferService> _logger;
    public ExternalTransferService(MoodleExternalDatabaseIntegrationDbContext dbContext, ILogger<ExternalTransferService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    // This is where you could load external data from whatever source, e.g. a CMS API endpoint
    private List<ExternalCourseDTO> GetStaticData()
    {
        List<ExternalCourseDTO> ret = new();

        var newCourse = new ExternalCourseDTO()
        {
            FullName = "Testkurs",
            ShortName = "TEST",
            Users = new List<ExternalUserDTO>()
        };

        var newUser = new ExternalUserDTO()
        {
            UserName = "test",
            EMail = "test@ruhr-uni-bochum.de"
        };

        newCourse.Users.Add(newUser);

        ret.Add(newCourse);

        return ret;
    }

    public async Task<bool> GetExternalDataAsync()
    {
        // GET SPOOF DATA
        var data = this.GetStaticData();
        var dbData = await _dbContext.ExternalTransferCourses.Include(i => i.CourseUsers).ThenInclude(i => i.ExternalTransferUser).ToListAsync();

        foreach (var course in data)
        {
            var dbCourse = dbData.FirstOrDefault(f => f.FullName == course.FullName);

            if (dbCourse == null)
            {
                _logger.LogInformation("Creating new Course {name}.", course.FullName);

                var newCourse = new ExternalTransferCourse()
                {
                    FullName = course.FullName,
                    ShortName = course.ShortName
                };

                await _dbContext.ExternalTransferCourses.AddAsync(newCourse);

                dbCourse = newCourse;
            }
            else
            {
                _logger.LogInformation("Course {name} already exists, skipping.", course.FullName);
            }

            if (dbCourse.CourseUsers == null) dbCourse.CourseUsers = new List<ExternalTransferCourseUser>();

            foreach (var user in course.Users)
            {
                var dbUser = await _dbContext.ExternalTransferUsers.FirstOrDefaultAsync(f => f.UserName == user.UserName);

                if (dbUser == null)
                {
                    _logger.LogInformation("Creating new User {name}.", user.UserName);

                    var newUser = new ExternalTransferUser()
                    {
                        UserName = user.UserName,
                        EMail = user.EMail
                    };

                    dbUser = newUser;
                }
                else
                {
                    _logger.LogInformation("User {name} already exists, skipping.", user.UserName);
                }

                if (!dbCourse.CourseUsers.Any(a => a.ExternalTransferUser?.UserName == dbUser.UserName))
                {
                    _logger.LogInformation("Adding User {username} to Course {coursename}.", dbUser.UserName, dbCourse.FullName);

                    dbCourse.CourseUsers.Add(new ExternalTransferCourseUser()
                    {
                        ExternalTransferCourse = dbCourse,
                        ExternalTransferUser = dbUser
                    });
                }
                else
                {
                    _logger.LogInformation("User {username} is already present in Course {coursename}.", dbUser.UserName, dbCourse.FullName);
                }
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error updating Database for Course {coursename}.", dbCourse.FullName);

                return false;
            }
        }

        return true;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Scoped External Transfer Service is working.");

            var success = await this.GetExternalDataAsync();

            _logger.LogInformation("External Transfer Success: {success}.", success);

            await Task.Delay(10000, stoppingToken);
        }
    }
}