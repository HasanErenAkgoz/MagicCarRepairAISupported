using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.ToTable("Taxes");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TaxType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(t => t.Month)
                .HasMaxLength(2); // 1-12, nullable

            builder.Property(t => t.Year)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(t => t.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(t => t.DueDate)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(TaxStatus.Pending);

            builder.Property(t => t.PaymentMethod)
                .HasConversion<int>();

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.PaymentReferenceNumber)
                .HasMaxLength(100);

            builder.Property(t => t.TaxOffice)
                .HasMaxLength(200);

            builder.Property(t => t.TaxNumber)
                .HasMaxLength(50);

            // Indexes
            builder.HasIndex(t => t.ClientId);
            builder.HasIndex(t => t.TaxType);
            builder.HasIndex(t => t.Status);
            builder.HasIndex(t => t.DueDate);
            builder.HasIndex(t => new { t.Year, t.Month });
            builder.HasIndex(t => new { t.Status, t.DueDate }); // Vadesi geçen vergiler için

            // Relationships
            builder.HasOne(t => t.Client)
                .WithMany()
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
