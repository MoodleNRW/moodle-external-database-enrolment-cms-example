using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moodle_external_database_integration.Core.Models.Moodle;

namespace moodle_external_database_integration.Data.Configurations;
public class MoodleUserConfiguration : IEntityTypeConfiguration<MoodleUser>
{
    public void Configure(EntityTypeBuilder<MoodleUser> builder)
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
            .Property(p => p.Password)
            .HasColumnName("password");

        builder
            .Property(p => p.EMail)
            .HasColumnName("e_mail");

        builder
            .Property(p => p.MoodleId)
            .HasColumnName("moodle_id");

        builder
            .Property(p => p.IdNumber)
            .HasColumnName("id_number")
            .HasComputedColumnSql("CAST(id AS text)", stored: true);

        builder
            .Property(p => p.ExternalTransferUserId)
            .HasColumnName("external_transfer_user_id");

        builder
            .HasOne(h => h.ExternalTransferUser)
            .WithOne(w => w.MoodleUser)
            .HasForeignKey<MoodleUser>(hfk => hfk.ExternalTransferUserId);

        builder.ToTable("moodle_users");
    }
}
