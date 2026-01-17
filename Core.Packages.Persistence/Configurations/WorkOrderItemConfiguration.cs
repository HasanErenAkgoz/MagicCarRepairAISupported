using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class WorkOrderItemConfiguration : IEntityTypeConfiguration<WorkOrderItem>
    {
        public void Configure(EntityTypeBuilder<WorkOrderItem> builder)
        {
            builder.ToTable("WorkOrderItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ItemType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(i => i.Quantity)
                .IsRequired()
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(1);

            builder.Property(i => i.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.DiscountAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(i => i.DiscountPercentage)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(i => i.TaxRate)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(20);

            builder.Property(i => i.TaxAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(i => i.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(i => i.BrandType)
                .HasConversion<int>();

            builder.Property(i => i.Notes)
                .HasMaxLength(1000);

            // Indexes
            builder.HasIndex(i => i.WorkOrderId);
            builder.HasIndex(i => i.PartId);
            builder.HasIndex(i => i.ClientId);
            builder.HasIndex(i => i.ItemType);

            // Relationships
            builder.HasOne(i => i.Client)
                .WithMany()
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.WorkOrder)
                .WithMany(w => w.Items)
                .HasForeignKey(i => i.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Part)
                .WithMany()
                .HasForeignKey(i => i.PartId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

