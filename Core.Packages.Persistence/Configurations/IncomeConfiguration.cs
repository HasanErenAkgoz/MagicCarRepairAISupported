using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("Incomes");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.TransactionDate)
                .IsRequired();

            builder.Property(i => i.Description)
                .HasMaxLength(1000);

            builder.Property(i => i.InvoiceNumber)
                .HasMaxLength(50);

            builder.HasIndex(i => i.ClientId);
            builder.HasIndex(i => i.TransactionDate);
            builder.HasIndex(i => i.WorkOrderId);
            builder.HasIndex(i => i.CustomerId);

            builder.HasOne(i => i.WorkOrder)
                .WithMany()
                .HasForeignKey(i => i.WorkOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Client)
                .WithMany()
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

