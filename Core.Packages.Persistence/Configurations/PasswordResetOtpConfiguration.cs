using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class PasswordResetOtpConfiguration : IEntityTypeConfiguration<PasswordResetOtp>
    {
        public void Configure(EntityTypeBuilder<PasswordResetOtp> builder)
        {
            builder.ToTable("PasswordResetOtps");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.OtpHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.ResetToken)
                .HasMaxLength(500);

            builder.Property(p => p.ExpiresAt)
                .IsRequired();

            builder.Property(p => p.Used)
                .HasDefaultValue(false);

            builder.Property(p => p.Attempts)
                .HasDefaultValue(0);

            // Indexes
            builder.HasIndex(p => new { p.UserId, p.Email });
            builder.HasIndex(p => p.ExpiresAt);
            builder.HasIndex(p => p.ResetToken);

            // Relationships
            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
