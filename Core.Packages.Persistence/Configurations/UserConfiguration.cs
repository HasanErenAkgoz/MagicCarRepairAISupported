using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // UserType
            builder.Property(x => x.UserType)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(UserType.Employee);

            // 2FA Fields
            builder.Property(x => x.TwoFactorEnabled)
                .IsRequired();

            builder.Property(x => x.TwoFactorSecret)
                .HasMaxLength(500);

            builder.Property(x => x.RecoveryCodes)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.HasCompletedOnboarding)
                .IsRequired();

            // Indexes
            builder.HasIndex(x => x.UserType);
            builder.HasIndex(x => x.TwoFactorEnabled);
        }
    }
}
