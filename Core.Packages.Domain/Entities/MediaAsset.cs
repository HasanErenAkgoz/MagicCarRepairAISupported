using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities;

/// <summary>Short-lived, server-owned media used only by AI diagnosis drafts.</summary>
public class MediaAsset : BaseEntity<int>, IClientEntity
{
    public int ClientId { get; set; }
    public int OwnerUserId { get; set; }
    public string Purpose { get; set; } = "AiDiagnosisDraft";
    public string StorageKey { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Length { get; set; }
    public DateTime ExpiresAt { get; set; }
}
