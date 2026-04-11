using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("UserSessions");
            builder.HasKey(us => us.Id);

            builder.Property(us => us.UserId).IsRequired();
            builder.Property(us => us.TokenId).IsRequired().HasMaxLength(255);
            builder.Property(us => us.DeviceId).HasMaxLength(255);
            builder.Property(us => us.DeviceName).HasMaxLength(255);
            builder.Property(us => us.IpAddress).HasMaxLength(45); // IPv6 için yeterli
            builder.Property(us => us.UserAgent).HasMaxLength(500);
            builder.Property(us => us.IsRemembered).IsRequired().HasDefaultValue(false);
            builder.Property(us => us.ExpiresAt).IsRequired();
            builder.Property(us => us.LastActivityAt).IsRequired();

            builder.HasOne(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(us => us.Client)
                .WithMany()
                .HasForeignKey(us => us.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(us => us.UserId);
            builder.HasIndex(us => us.TokenId).IsUnique();
            builder.HasIndex(us => us.ExpiresAt);
            builder.HasIndex(us => us.DeviceId);
        }
    }
}
