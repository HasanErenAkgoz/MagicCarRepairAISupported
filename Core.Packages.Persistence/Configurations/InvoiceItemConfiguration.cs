using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.ToTable("InvoiceItems");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(i => i.Quantity)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.TaxRate)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(20);

            builder.Property(i => i.TaxAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasIndex(i => i.ClientId);
            builder.HasIndex(i => i.InvoiceId);

            builder.HasOne(i => i.Invoice)
                .WithMany(inv => inv.Items)
                .HasForeignKey(i => i.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Client)
                .WithMany()
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

