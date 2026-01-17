using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.TransactionDate)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(1000);

            builder.Property(e => e.SupplierName)
                .HasMaxLength(200);

            builder.Property(e => e.InvoiceNumber)
                .HasMaxLength(50);

            builder.HasIndex(e => e.ClientId);
            builder.HasIndex(e => e.TransactionDate);
            builder.HasIndex(e => e.EmployeeId);
            builder.HasIndex(e => e.ExpenseType);

            builder.HasOne(e => e.Employee)
                .WithMany()
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

