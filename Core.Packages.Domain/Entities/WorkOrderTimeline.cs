using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// İş emri timeline (işlem geçmişi) entity'si
    /// </summary>
    public class WorkOrderTimeline : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// İş emri ID
        /// </summary>
        public int WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// Tarih/Saat
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Durum Değişikliği (Eski durumdan yeni duruma)
        /// </summary>
        public WorkOrderStatus? StatusChange { get; set; }

        /// <summary>
        /// Eski Durum
        /// </summary>
        public WorkOrderStatus? OldStatus { get; set; }

        /// <summary>
        /// Yeni Durum
        /// </summary>
        public WorkOrderStatus? NewStatus { get; set; }

        /// <summary>
        /// Yapan Personel ID
        /// </summary>
        public int? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Fotoğraf ID'leri (JSON formatında - WorkOrderPhoto ID'leri)
        /// </summary>
        public string? PhotoIds { get; set; }

        /// <summary>
        /// Event Tipi (StatusChange, Note, Photo, vb.)
        /// </summary>
        public string EventType { get; set; } = "StatusChange";

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}

