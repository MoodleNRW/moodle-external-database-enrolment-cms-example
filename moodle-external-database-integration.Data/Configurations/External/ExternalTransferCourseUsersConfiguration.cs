using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moodle_external_database_integration.Core.Models.External;

namespace moodle_external_database_integration.Data.Configurations;
public class ExternalTransferCourseUserConfiguration : IEntityTypeConfiguration<ExternalTransferCourseUser>
{
    public void Configure(EntityTypeBuilder<ExternalTransferCourseUser> builder)
    {
        builder
            .HasKey(k => new { k.ExternalTransferCourseId, k.ExternalTransferUserId });

        builder
            .Property(p => p.ExternalTransferCourseId)
            .HasColumnName("external_transfer_course_id");

        builder
            .Property(p => p.ExternalTransferUserId)
            .HasColumnName("external_transfer_user_id");

        builder
            .HasOne(h => h.ExternalTransferCourse)
            .WithMany(w => w.CourseUsers)
            .HasForeignKey(h => h.ExternalTransferCourseId);

        builder
            .HasOne(h => h.ExternalTransferUser)
            .WithMany(w => w.UserCourses)
            .HasForeignKey(h => h.ExternalTransferUserId);


        builder.ToTable("external_transfer_courses_users");
    }
}
