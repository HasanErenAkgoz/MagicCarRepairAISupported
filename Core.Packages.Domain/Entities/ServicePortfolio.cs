using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Servis portföyü entity'si - Tamamlanan iş emirlerinin portföyde sergilenmesi için
    /// </summary>
    public class ServicePortfolio : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// İş Emri ID (referans)
        /// </summary>
        public int WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// Başlık (müşteri bilgileri gizlenir)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Kategori/Etiketler (JSON formatında: ["Motor Tamiri", "Kaporta", "Boya", ...])
        /// </summary>
        public string? Categories { get; set; }

        /// <summary>
        /// Ön plana çıkan fotoğraflar (JSON array formatında - WorkOrderPhoto ID'leri)
        /// </summary>
        public string? FeaturedPhotoIds { get; set; }

        /// <summary>
        /// Yayında mı?
        /// </summary>
        public bool IsPublished { get; set; } = false;

        /// <summary>
        /// Yayın tarihi
        /// </summary>
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// Müşteri onay durumu
        /// </summary>
        public CustomerApprovalStatus CustomerApprovalStatus { get; set; } = CustomerApprovalStatus.Pending;

        /// <summary>
        /// Müşteri onay tarihi
        /// </summary>
        public DateTime? CustomerApprovalDate { get; set; }

        /// <summary>
        /// Müşteri red nedeni
        /// </summary>
        public string? CustomerRejectionReason { get; set; }

        /// <summary>
        /// Görüntülenme sayısı
        /// </summary>
        public int ViewCount { get; set; } = 0;

        /// <summary>
        /// Beğeni sayısı
        /// </summary>
        public int LikeCount { get; set; } = 0;

        /// <summary>
        /// Sıralama (önem sırasına göre)
        /// </summary>
        public int DisplayOrder { get; set; } = 0;

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
