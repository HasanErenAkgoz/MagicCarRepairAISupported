using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Teklif talebi fotoğrafı entity'si
    /// </summary>
    public class QuoteRequestPhoto : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Teklif talebi ID
        /// </summary>
        public int QuoteRequestId { get; set; }
        public virtual QuoteRequest QuoteRequest { get; set; }

        /// <summary>
        /// Dosya yolu
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// UploadedFile ID (opsiyonel)
        /// </summary>
        public int? UploadedFileId { get; set; }
        public virtual UploadedFile? UploadedFile { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Fotoğraf tipi
        /// </summary>
        public QuoteRequestPhotoType PhotoType { get; set; } = QuoteRequestPhotoType.General;

        /// <summary>
        /// Yükleme tarihi
        /// </summary>
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Yükleyen personel ID (opsiyonel)
        /// </summary>
        public int? UploadedByEmployeeId { get; set; }
        public virtual Employee? UploadedByEmployee { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}

