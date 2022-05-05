using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moodle_external_database_integration.Core.Models.Moodle;

namespace moodle_external_database_integration.Data.Configurations;
public class MoodleEnrolmentConfiguration : IEntityTypeConfiguration<MoodleEnrolment>
{
    public void Configure(EntityTypeBuilder<MoodleEnrolment> builder)
    {
        builder
            .HasKey(k => k.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("id");

        builder
            .Property(p => p.UserIdNumber)
            .HasColumnName("user_id_number")
            .HasComputedColumnSql("CAST(moodle_user_id AS text)", stored: true);

        builder
            .Property(p => p.CourseIdNumber)
            .HasColumnName("course_id_number")
            .HasComputedColumnSql("CAST(moodle_course_id AS text)", stored: true);

        builder
            .Property(p => p.RoleId)
            .HasColumnName("role_id");

        builder
            .Property(p => p.MoodleUserId)
            .HasColumnName("moodle_user_id")
            .IsRequired();

        builder
            .HasOne(h => h.MoodleUser)
            .WithMany(w => w.MoodleEnrolments);

        builder
            .Property(p => p.MoodleCourseId)
            .HasColumnName("moodle_course_id")
            .IsRequired();

        builder
            .HasOne(h => h.MoodleCourse)
            .WithMany(w => w.MoodleEnrolments);

        builder
            .Property(p => p.ExternalTransferUserId)
            .HasColumnName("external_transfer_user_id");

        builder
            .HasOne(h => h.ExternalTransferUser)
            .WithMany(w => w.MoodleEnrolments);

        builder
            .Property(p => p.ExternalTransferCourseId)
            .HasColumnName("external_transfer_course_id");

        builder
            .HasOne(h => h.ExternalTransferCourse)
            .WithMany(w => w.MoodleEnrolments);

        builder.ToTable("moodle_enrolments");
    }
}
