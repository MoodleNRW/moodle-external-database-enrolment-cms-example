using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moodle_external_database_integration.Core.Models.External;

namespace moodle_external_database_integration.Data.Configurations;
public class ExternalTransferCourseConfiguration : IEntityTypeConfiguration<ExternalTransferCourse>
{
    public void Configure(EntityTypeBuilder<ExternalTransferCourse> builder)
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
            .Property(p => p.ShortName)
            .HasColumnName("short_name");

        builder.ToTable("external_transfer_courses");
    }
}
