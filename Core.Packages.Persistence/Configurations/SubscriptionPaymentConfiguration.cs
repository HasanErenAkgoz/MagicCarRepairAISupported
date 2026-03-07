using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class SubscriptionPaymentConfiguration : IEntityTypeConfiguration<SubscriptionPayment>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPayment> builder)
        {
            builder.ToTable("SubscriptionPayments");
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.SubscriptionId)
                .IsRequired();

            builder.Property(sp => sp.PaymentNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sp => sp.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(sp => sp.PaymentStatus)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(PaymentStatus.Unpaid);

            builder.Property(sp => sp.PaymentMethod)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(sp => sp.PaymentDate)
                .IsRequired();

            builder.Property(sp => sp.PaymentGateway)
                .HasConversion<int>();

            builder.Property(sp => sp.GatewayPaymentId)
                .HasMaxLength(200);

            builder.Property(sp => sp.GatewayConversationId)
                .HasMaxLength(200);

            builder.Property(sp => sp.PaymentPeriod)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Monthly");

            builder.Property(sp => sp.Description)
                .HasMaxLength(1000);

            builder.Property(sp => sp.GatewayResponseMessage)
                .HasMaxLength(1000);

            builder.Property(sp => sp.GatewayResponseCode)
                .HasMaxLength(50);

            builder.Property(sp => sp.ClientId)
                .IsRequired();

            // Relationships
            builder.HasOne(sp => sp.Subscription)
                .WithMany(s => s.Payments)
                .HasForeignKey(sp => sp.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sp => sp.Client)
                .WithMany()
                .HasForeignKey(sp => sp.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(sp => sp.SubscriptionId);
            builder.HasIndex(sp => sp.PaymentNumber)
                .IsUnique();
            builder.HasIndex(sp => sp.ClientId);
            builder.HasIndex(sp => sp.PaymentDate);
        }
    }
}
