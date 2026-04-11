using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Parça fotoğraf entity'si
    /// </summary>
    public class PartPhoto : BaseEntity<int>, IClientEntity
    {
        public int PartId { get; set; }
        public virtual Part Part { get; set; }

        public string FilePath { get; set; }

        public int? UploadedFileId { get; set; }
        public virtual UploadedFile? UploadedFile { get; set; }

        public string? Description { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public int DisplayOrder { get; set; } = 0;

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
