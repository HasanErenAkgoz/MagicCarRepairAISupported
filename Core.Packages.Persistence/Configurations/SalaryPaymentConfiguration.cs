using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class SalaryPaymentConfiguration : IEntityTypeConfiguration<SalaryPayment>
    {
        public void Configure(EntityTypeBuilder<SalaryPayment> builder)
        {
            builder.ToTable("SalaryPayments");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Month)
                .IsRequired()
                .HasMaxLength(2); // 1-12

            builder.Property(s => s.Year)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(s => s.GrossSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.SocialSecurityDeduction)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.UnemploymentInsuranceDeduction)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.IncomeTaxDeduction)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.StampTax)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.OtherDeductions)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(s => s.NetSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.PaymentDate)
                .IsRequired();

            builder.Property(s => s.PaymentMethod)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(s => s.Description)
                .HasMaxLength(1000);

            builder.Property(s => s.PaymentReferenceNumber)
                .HasMaxLength(100);

            // Indexes
            builder.HasIndex(s => s.ClientId);
            builder.HasIndex(s => s.EmployeeId);
            builder.HasIndex(s => new { s.Year, s.Month });
            builder.HasIndex(s => s.PaymentDate);
            builder.HasIndex(s => new { s.EmployeeId, s.Year, s.Month })
                .IsUnique(); // Her personel için aynı ay/yıl'da bir ödeme

            // Relationships
            builder.HasOne(s => s.Employee)
                .WithMany()
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Client)
                .WithMany()
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
