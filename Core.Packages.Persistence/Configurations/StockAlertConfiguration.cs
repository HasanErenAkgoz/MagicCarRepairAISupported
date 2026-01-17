using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class StockAlertConfiguration : IEntityTypeConfiguration<StockAlert>
    {
        public void Configure(EntityTypeBuilder<StockAlert> builder)
        {
            builder.ToTable("StockAlerts");

            builder.HasKey(sa => sa.Id);

            builder.Property(sa => sa.Message)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(sa => sa.AlertType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(sa => sa.Status)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(StockAlertStatus.Active);

            // Relationships
            builder.HasOne(sa => sa.Part)
                .WithMany()
                .HasForeignKey(sa => sa.PartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sa => sa.PartStock)
                .WithMany()
                .HasForeignKey(sa => sa.PartStockId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sa => sa.Client)
                .WithMany()
                .HasForeignKey(sa => sa.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(sa => sa.PartId);
            builder.HasIndex(sa => sa.PartStockId);
            builder.HasIndex(sa => sa.ClientId);
            builder.HasIndex(sa => sa.Status);
            builder.HasIndex(sa => sa.AlertType);
            builder.HasIndex(sa => sa.AutoOrderId);
        }
    }
}

