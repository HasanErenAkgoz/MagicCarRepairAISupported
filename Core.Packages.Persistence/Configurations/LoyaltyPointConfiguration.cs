using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class LoyaltyPointConfiguration : IEntityTypeConfiguration<LoyaltyPoint>
    {
        public void Configure(EntityTypeBuilder<LoyaltyPoint> builder)
        {
            builder.ToTable("LoyaltyPoints");
            builder.HasKey(lp => lp.Id);

            builder.Property(lp => lp.Points)
                .IsRequired();

            builder.Property(lp => lp.Type)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(lp => lp.Description)
                .IsRequired()
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(lp => lp.Customer)
                .WithMany()
                .HasForeignKey(lp => lp.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lp => lp.WorkOrder)
                .WithMany()
                .HasForeignKey(lp => lp.WorkOrderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(lp => lp.Reward)
                .WithMany()
                .HasForeignKey(lp => lp.RewardId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(lp => lp.Client)
                .WithMany()
                .HasForeignKey(lp => lp.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(lp => lp.CustomerId);
            builder.HasIndex(lp => lp.WorkOrderId);
            builder.HasIndex(lp => new { lp.CustomerId, lp.Type });
        }
    }
}
