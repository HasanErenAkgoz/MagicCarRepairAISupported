using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class UserDeviceTokenConfiguration : IEntityTypeConfiguration<UserDeviceToken>
    {
        public void Configure(EntityTypeBuilder<UserDeviceToken> builder)
        {
            builder.ToTable("UserDeviceTokens");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.DeviceType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.DeviceModel)
                .HasMaxLength(100);

            builder.Property(t => t.DeviceOSVersion)
                .HasMaxLength(50);

            // Relationships
            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Client)
                .WithMany()
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(t => t.UserId);
            builder.HasIndex(t => t.Token)
                .IsUnique();
            builder.HasIndex(t => new { t.UserId, t.IsActive });
        }
    }
}
