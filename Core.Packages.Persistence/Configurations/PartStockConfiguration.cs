using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class PartStockConfiguration : IEntityTypeConfiguration<PartStock>
    {
        public void Configure(EntityTypeBuilder<PartStock> builder)
        {
            builder.ToTable("PartStocks");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.Location)
                .HasMaxLength(100);

            builder.Property(s => s.LastUpdatedDate);

            // Multi-tenant
            builder.HasIndex(s => s.PartId).IsUnique(); // Her parça için tek stok kaydı
            builder.HasIndex(s => s.ClientId);
            builder.HasIndex(s => s.Quantity);
            builder.HasIndex(s => s.Status);

            // Composite indexes for query optimization
            builder.HasIndex(s => new { s.ClientId, s.Quantity }); // Low stock queries
            builder.HasIndex(s => new { s.ClientId, s.Status, s.Quantity }); // Active stock queries

            builder.HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Part)
                .WithOne(p => p.Stock)
                .HasForeignKey<PartStock>(s => s.PartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.LastUpdatedByEmployee)
                .WithMany()
                .HasForeignKey(s => s.LastUpdatedByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

