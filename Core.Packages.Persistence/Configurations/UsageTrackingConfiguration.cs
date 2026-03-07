using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class UsageTrackingConfiguration : IEntityTypeConfiguration<UsageTracking>
    {
        public void Configure(EntityTypeBuilder<UsageTracking> builder)
        {
            builder.ToTable("UsageTracking");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.ClientId)
                .IsRequired();

            builder.Property(u => u.LimitType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(u => u.CurrentPeriod)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(u => u.UsageCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(u => u.LimitAmount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(u => u.ResetDate)
                .IsRequired();

            // Relationships
            builder.HasOne(u => u.Client)
                .WithMany()
                .HasForeignKey(u => u.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(u => u.ClientId);
            builder.HasIndex(u => new { u.ClientId, u.LimitType, u.CurrentPeriod })
                .IsUnique();
        }
    }
}
