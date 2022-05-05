using Microsoft.EntityFrameworkCore;
using moodle_external_database_integration.Core.Models.External;
using moodle_external_database_integration.Core.Models.Moodle;
using moodle_external_database_integration.Data.Configurations;

namespace moodle_external_database_integration.Data;
public class MoodleExternalDatabaseIntegrationDbContext : DbContext
{
    public DbSet<ExternalTransferCourse> ExternalTransferCourses { get; set; }
    public DbSet<ExternalTransferCourseUser> ExternalTransferCoursesUsers { get; set; }
    public DbSet<ExternalTransferUser> ExternalTransferUsers { get; set; }
    public DbSet<MoodleCourse> MoodleCourses { get; set; }
    public DbSet<MoodleEnrolment> MoodleEnrolments { get; set; }
    public DbSet<MoodleUser> MoodleUsers { get; set; }
    public MoodleExternalDatabaseIntegrationDbContext(DbContextOptions<MoodleExternalDatabaseIntegrationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ExternalTransferCourseConfiguration());
        modelBuilder.ApplyConfiguration(new ExternalTransferCourseUserConfiguration());
        modelBuilder.ApplyConfiguration(new ExternalTransferUserConfiguration());

        modelBuilder.ApplyConfiguration(new MoodleCourseConfiguration());
        modelBuilder.ApplyConfiguration(new MoodleEnrolmentConfiguration());
        modelBuilder.ApplyConfiguration(new MoodleUserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}