using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
    {
        public void Configure(EntityTypeBuilder<UserDevice> builder)
        {
            builder.ToTable("UserDevices");
            builder.HasKey(ud => ud.Id);

            builder.Property(ud => ud.UserId).IsRequired();
            builder.Property(ud => ud.DeviceId).IsRequired().HasMaxLength(255);
            builder.Property(ud => ud.DeviceName).HasMaxLength(255);
            builder.Property(ud => ud.IsTrusted).IsRequired().HasDefaultValue(false);
            builder.Property(ud => ud.LastLoginAt).IsRequired();

            builder.HasOne(ud => ud.User)
                .WithMany()
                .HasForeignKey(ud => ud.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ud => ud.Client)
                .WithMany()
                .HasForeignKey(ud => ud.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(ud => ud.UserId);
            builder.HasIndex(ud => ud.DeviceId);
            builder.HasIndex(ud => new { ud.UserId, ud.DeviceId }).IsUnique();
        }
    }
}
