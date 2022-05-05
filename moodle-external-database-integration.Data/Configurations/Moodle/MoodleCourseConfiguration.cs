using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moodle_external_database_integration.Core.Models.Moodle;

namespace moodle_external_database_integration.Data.Configurations;
public class MoodleCourseConfiguration : IEntityTypeConfiguration<MoodleCourse>
{
    public void Configure(EntityTypeBuilder<MoodleCourse> builder)
    {
        builder
            .HasKey(k => k.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("id");

        builder
            .Property(p => p.FullName)
            .HasColumnName("full_name");

        builder
            .Property(p => p.MoodleId)
            .HasColumnName("moodle_id");

        builder
            .Property(p => p.IdNumber)
            .HasColumnName("id_number")
            .HasComputedColumnSql("CAST(id AS text)", stored: true);

        builder
            .Property(p => p.ShortName)
            .HasColumnName("short_name");

        builder
            .Property(p => p.ExternalTransferCourseId)
            .HasColumnName("external_transfer_course_id");

        builder
            .HasOne(h => h.ExternalTransferCourse)
            .WithOne(w => w.MoodleCourse)
            .HasForeignKey<MoodleCourse>(hfk => hfk.ExternalTransferCourseId);

        builder.ToTable("moodle_courses");
    }
}
