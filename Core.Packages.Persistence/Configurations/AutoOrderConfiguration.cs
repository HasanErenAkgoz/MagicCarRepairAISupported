using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class AutoOrderConfiguration : IEntityTypeConfiguration<AutoOrder>
    {
        public void Configure(EntityTypeBuilder<AutoOrder> builder)
        {
            builder.ToTable("AutoOrders");

            builder.HasKey(ao => ao.Id);

            builder.Property(ao => ao.OrderNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ao => ao.Status)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(AutoOrderStatus.Pending);

            // Relationships
            builder.HasOne(ao => ao.Part)
                .WithMany()
                .HasForeignKey(ao => ao.PartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ao => ao.PartSupplier)
                .WithMany()
                .HasForeignKey(ao => ao.PartSupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ao => ao.StockAlert)
                .WithMany()
                .HasForeignKey(ao => ao.StockAlertId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ao => ao.ApprovedByUser)
                .WithMany()
                .HasForeignKey(ao => ao.ApprovedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ao => ao.Client)
                .WithMany()
                .HasForeignKey(ao => ao.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(ao => ao.OrderNumber)
                .IsUnique();

            builder.HasIndex(ao => ao.PartId);
            builder.HasIndex(ao => ao.ClientId);
            builder.HasIndex(ao => ao.Status);
            builder.HasIndex(ao => ao.StockAlertId);
        }
    }
}

