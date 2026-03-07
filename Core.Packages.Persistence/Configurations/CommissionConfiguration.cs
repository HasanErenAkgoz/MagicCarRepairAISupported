using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class CommissionConfiguration : IEntityTypeConfiguration<Commission>
    {
        public void Configure(EntityTypeBuilder<Commission> builder)
        {
            builder.ToTable("Commissions");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.PaymentId)
                .IsRequired();

            builder.Property(c => c.CommissionAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.CommissionRate)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(CommissionStatus.Pending);

            builder.Property(c => c.RefundReason)
                .HasMaxLength(1000);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.Property(c => c.ClientId)
                .IsRequired();

            // Relationships
            builder.HasOne(c => c.Payment)
                .WithMany(p => p.Commissions)
                .HasForeignKey(c => c.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(c => c.PaymentId);
            builder.HasIndex(c => c.ClientId);
            builder.HasIndex(c => c.Status);
            builder.HasIndex(c => c.PaymentDate);
        }
    }
}
