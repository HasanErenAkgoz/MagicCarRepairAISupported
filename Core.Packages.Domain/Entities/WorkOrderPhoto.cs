using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// İş emri fotoğraf entity'si
    /// </summary>
    public class WorkOrderPhoto : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// İş emri ID
        /// </summary>
        public int WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// Timeline ID (opsiyonel - hangi timeline event'ine ait)
        /// </summary>
        public int? TimelineId { get; set; }
        public virtual WorkOrderTimeline? Timeline { get; set; }

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
        /// Tip (Giriş, İşlem, Çıkış, Arıza, vb.)
        /// </summary>
        public WorkOrderPhotoType PhotoType { get; set; } = WorkOrderPhotoType.Process;

        /// <summary>
        /// Yükleme Tarihi
        /// </summary>
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Yükleyen Personel ID
        /// </summary>
        public int? UploadedByEmployeeId { get; set; }
        public virtual Employee? UploadedByEmployee { get; set; }

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}

