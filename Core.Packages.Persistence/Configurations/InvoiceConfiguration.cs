using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.SubTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.TaxAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.PaidAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(i => i.Description)
                .HasMaxLength(1000);

            builder.Property(i => i.EInvoiceId)
                .HasMaxLength(100);

            builder.HasIndex(i => i.ClientId);
            builder.HasIndex(i => i.InvoiceNumber);
            builder.HasIndex(i => new { i.InvoiceNumber, i.ClientId }).IsUnique();
            builder.HasIndex(i => i.InvoiceDate);
            builder.HasIndex(i => i.DueDate);
            builder.HasIndex(i => i.WorkOrderId);
            builder.HasIndex(i => i.CustomerId);
            builder.HasIndex(i => i.Status);

            // Composite indexes for query optimization
            builder.HasIndex(i => new { i.ClientId, i.InvoiceDate }); // Date range queries
            builder.HasIndex(i => new { i.ClientId, i.Status, i.DueDate }); // Overdue invoices
            builder.HasIndex(i => new { i.ClientId, i.CustomerId, i.InvoiceDate }); // Customer invoices

            builder.HasOne(i => i.WorkOrder)
                .WithMany()
                .HasForeignKey(i => i.WorkOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Supplier)
                .WithMany()
                .HasForeignKey(i => i.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Client)
                .WithMany()
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Items)
                .WithOne(item => item.Invoice)
                .HasForeignKey(item => item.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

