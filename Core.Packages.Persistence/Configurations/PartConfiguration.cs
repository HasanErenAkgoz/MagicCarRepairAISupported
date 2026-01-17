using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable("Parts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PartCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.BrandType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.Brand)
                .HasMaxLength(100);

            builder.Property(p => p.OEMNumber)
                .HasMaxLength(50);

            builder.Property(p => p.Barcode)
                .HasMaxLength(100);

            builder.Property(p => p.PurchasePrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.SalePrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.TaxRate)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(20);

            builder.Property(p => p.MinimumStockLevel)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(p => p.IsLowStockAlertEnabled)
                .HasDefaultValue(true);

            builder.Property(p => p.Unit)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Adet");

            builder.Property(p => p.WarrantyMonths);

            builder.Property(p => p.Notes)
                .HasMaxLength(1000);

            // Multi-tenant
            builder.HasIndex(p => new { p.PartCode, p.ClientId }).IsUnique();
            builder.HasIndex(p => p.ClientId);
            builder.HasIndex(p => p.Category);
            builder.HasIndex(p => p.BrandType);
            builder.HasIndex(p => p.Barcode);
            builder.HasIndex(p => p.SupplierId);
            builder.HasIndex(p => p.Status);

            // Composite indexes for query optimization
            builder.HasIndex(p => new { p.ClientId, p.Category, p.Status });
            builder.HasIndex(p => new { p.ClientId, p.BrandType, p.Status });
            builder.HasIndex(p => new { p.ClientId, p.IsLowStockAlertEnabled, p.Status });
            builder.HasIndex(p => new { p.ClientId, p.Name }); // For search queries

            builder.HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Parts)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

