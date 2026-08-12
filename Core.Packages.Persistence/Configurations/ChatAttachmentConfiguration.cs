using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations;

public sealed class ChatAttachmentConfiguration : IEntityTypeConfiguration<ChatAttachment>
{
    public void Configure(EntityTypeBuilder<ChatAttachment> builder)
    {
        builder.ToTable("ChatAttachments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StorageKey).IsRequired().HasMaxLength(500);
        builder.Property(x => x.OriginalFileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ContentType).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => new { x.ClientId, x.WorkOrderId, x.ChatMessageId });
        builder.HasIndex(x => new { x.ClientId, x.ExpiresAt });
        builder.HasOne(x => x.ChatMessage).WithMany(x => x.Attachments)
            .HasForeignKey(x => x.ChatMessageId).OnDelete(DeleteBehavior.Cascade);
    }
}
