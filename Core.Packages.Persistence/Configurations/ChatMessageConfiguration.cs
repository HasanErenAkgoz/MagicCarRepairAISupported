using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Message)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(c => c.FilePath)
                .HasMaxLength(500);

            builder.Property(c => c.FileName)
                .HasMaxLength(255);

            // Indexes
            builder.HasIndex(c => c.SenderId);
            builder.HasIndex(c => c.ReceiverId);
            builder.HasIndex(c => c.WorkOrderId);
            builder.HasIndex(c => c.CustomerId);
            builder.HasIndex(c => c.SentDate);
            builder.HasIndex(c => c.ClientId);
            builder.HasIndex(c => new { c.SenderId, c.ReceiverId, c.SentDate });
            builder.HasIndex(c => new { c.WorkOrderId, c.SentDate });

            // Relationships
            builder.HasOne(c => c.Sender)
                .WithMany()
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Receiver)
                .WithMany()
                .HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.WorkOrder)
                .WithMany()
                .HasForeignKey(c => c.WorkOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
