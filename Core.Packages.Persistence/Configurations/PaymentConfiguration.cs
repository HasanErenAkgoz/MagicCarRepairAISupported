using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.GatewayPaymentId)
                .HasMaxLength(100);

            builder.Property(p => p.GatewayConversationId)
                .HasMaxLength(100);

            builder.Property(p => p.CardLastFourDigits)
                .HasMaxLength(4);

            builder.Property(p => p.CardHolderName)
                .HasMaxLength(100);

            builder.Property(p => p.BankName)
                .HasMaxLength(100);

            builder.Property(p => p.GatewayResponseMessage)
                .HasMaxLength(500);

            builder.Property(p => p.GatewayResponseCode)
                .HasMaxLength(50);

            builder.Property(p => p.GatewayRefundId)
                .HasMaxLength(100);

            builder.Property(p => p.RefundDescription)
                .HasMaxLength(1000);

            builder.HasIndex(p => p.ClientId);
            builder.HasIndex(p => p.PaymentNumber);
            builder.HasIndex(p => new { p.PaymentNumber, p.ClientId }).IsUnique();
            builder.HasIndex(p => p.PaymentDate);
            builder.HasIndex(p => p.InvoiceId);
            builder.HasIndex(p => p.WorkOrderId);
            builder.HasIndex(p => p.CustomerId);
            builder.HasIndex(p => p.GatewayPaymentId);
            builder.HasIndex(p => p.GatewayConversationId);

            builder.HasOne(p => p.Invoice)
                .WithMany()
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.WorkOrder)
                .WithMany()
                .HasForeignKey(p => p.WorkOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}





