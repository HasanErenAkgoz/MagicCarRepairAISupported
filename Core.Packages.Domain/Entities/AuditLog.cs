using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Audit log entity'si - Tüm kritik işlemlerin loglanması için
    /// </summary>
    public class AuditLog : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Kullanıcı ID (işlemi yapan)
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Entity adı (örn: WorkOrder, Invoice, Customer)
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Entity ID
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// İşlem tipi (Create, Update, Delete, View, etc.)
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Eski değerler (JSON formatında)
        /// </summary>
        public string? OldValues { get; set; }

        /// <summary>
        /// Yeni değerler (JSON formatında)
        /// </summary>
        public string? NewValues { get; set; }

        /// <summary>
        /// Değişen property'ler (JSON array formatında)
        /// </summary>
        public string? ChangedProperties { get; set; }

        /// <summary>
        /// IP adresi
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// User Agent (tarayıcı/cihaz bilgisi)
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Hata mesajı (eğer başarısızsa)
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// İşlem süresi (milisaniye)
        /// </summary>
        public long? DurationMs { get; set; }

        /// <summary>
        /// Request path (API endpoint)
        /// </summary>
        public string? RequestPath { get; set; }

        /// <summary>
        /// Request method (GET, POST, PUT, DELETE)
        /// </summary>
        public string? RequestMethod { get; set; }

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
