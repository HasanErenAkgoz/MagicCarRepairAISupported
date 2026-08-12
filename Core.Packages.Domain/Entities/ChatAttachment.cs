using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities;

/// <summary>Private, tenant-owned chat upload. StorageKey is never exposed to clients.</summary>
public sealed class ChatAttachment : BaseEntity<int>, IClientEntity
{
    public int ClientId { get; set; }
    public int WorkOrderId { get; set; }
    public int OwnerUserId { get; set; }
    public int? ChatMessageId { get; set; }
    public string StorageKey { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Length { get; set; }
    public DateTime ExpiresAt { get; set; }
    public ChatMessage? ChatMessage { get; set; }
}
