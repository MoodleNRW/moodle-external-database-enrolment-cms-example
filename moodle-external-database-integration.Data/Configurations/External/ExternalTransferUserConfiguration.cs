using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moodle_external_database_integration.Core.Models.External;

namespace moodle_external_database_integration.Data.Configurations;
public class ExternalTransferUserConfiguration : IEntityTypeConfiguration<ExternalTransferUser>
{
    public void Configure(EntityTypeBuilder<ExternalTransferUser> builder)
    {
        builder
            .HasKey(k => k.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("id");

        builder
            .Property(p => p.UserName)
            .HasColumnName("user_name");

        builder
            .Property(p => p.EMail)
            .HasColumnName("e_mail");


        builder.ToTable("external_transfer_users");
    }
}
