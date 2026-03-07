using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.ClientId)
                .IsRequired();

            builder.Property(s => s.Plan)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(SubscriptionStatus.Active);

            builder.Property(s => s.StartDate)
                .IsRequired();

            builder.Property(s => s.EndDate)
                .IsRequired();

            builder.Property(s => s.MonthlyPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.YearlyPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.MaxWorkOrders)
                .IsRequired()
                .HasDefaultValue(10);

            builder.Property(s => s.MaxUsers)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(s => s.MaxAIAnalyses)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.Features)
                .HasColumnType("nvarchar(max)");

            builder.Property(s => s.AutoRenew)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(s => s.CancellationReason)
                .HasMaxLength(1000);

            // Relationships
            builder.HasOne(s => s.Client)
                .WithMany(c => c.Subscriptions)
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Payments)
                .WithOne(sp => sp.Subscription)
                .HasForeignKey(sp => sp.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(s => s.ClientId);
            builder.HasIndex(s => new { s.ClientId, s.Status });
            builder.HasIndex(s => s.EndDate);
        }
    }
}
