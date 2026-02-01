using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Araç fotoğraf entity'si
    /// </summary>
    public class VehiclePhoto : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Araç ID
        /// </summary>
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        /// <summary>
        /// Fotoğraf Yolu (UploadedFile ID veya dosya yolu)
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// UploadedFile ID (eğer UploadedFile kullanılıyorsa)
        /// </summary>
        public int? UploadedFileId { get; set; }
        public virtual UploadedFile? UploadedFile { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Fotoğraf Tipi (Ön, Arka, Yan, İç, Motor, vb.)
        /// </summary>
        public string? PhotoType { get; set; }

        /// <summary>
        /// Yükleme Tarihi
        /// </summary>
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Yükleyen Personel ID (opsiyonel)
        /// </summary>
        public int? UploadedByEmployeeId { get; set; }
        public virtual Employee? UploadedByEmployee { get; set; }

        /// <summary>
        /// Sıralama (gösterim sırası)
        /// </summary>
        public int DisplayOrder { get; set; } = 0;

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
