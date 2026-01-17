using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class PartSupplierConfiguration : IEntityTypeConfiguration<PartSupplier>
    {
        public void Configure(EntityTypeBuilder<PartSupplier> builder)
        {
            builder.ToTable("PartSuppliers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.ContactPerson)
                .HasMaxLength(100);

            builder.Property(s => s.Phone)
                .HasMaxLength(20);

            builder.Property(s => s.Email)
                .HasMaxLength(100);

            builder.Property(s => s.Address)
                .HasMaxLength(500);

            builder.Property(s => s.City)
                .HasMaxLength(50);

            builder.Property(s => s.Country)
                .HasMaxLength(50);

            builder.Property(s => s.TaxNumber)
                .HasMaxLength(20);

            builder.Property(s => s.TaxOffice)
                .HasMaxLength(100);

            builder.Property(s => s.PaymentTerms)
                .HasMaxLength(200);

            builder.Property(s => s.Notes)
                .HasMaxLength(1000);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);

            // Multi-tenant
            builder.HasIndex(s => new { s.CompanyName, s.ClientId }).IsUnique();
            builder.HasIndex(s => s.ClientId);

            builder.HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationships
            builder.HasMany(s => s.Parts)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

