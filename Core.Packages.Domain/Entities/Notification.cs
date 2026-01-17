using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Bildirim entity'si
    /// </summary>
    public class Notification : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Bildirim tipi
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Alıcı User ID (opsiyonel - Email/SMS için email/telefon kullanılabilir)
        /// </summary>
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        /// <summary>
        /// Alıcı Email (User yoksa)
        /// </summary>
        public string? RecipientEmail { get; set; }

        /// <summary>
        /// Alıcı Telefon (User yoksa)
        /// </summary>
        public string? RecipientPhone { get; set; }

        /// <summary>
        /// Başlık
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// İçerik
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Durum
        /// </summary>
        public NotificationStatus Status { get; set; } = NotificationStatus.Pending;

        /// <summary>
        /// Gönderim tarihi
        /// </summary>
        public DateTime? SentDate { get; set; }

        /// <summary>
        /// Okunma tarihi
        /// </summary>
        public DateTime? ReadDate { get; set; }

        /// <summary>
        /// Hata mesajı (başarısız olursa)
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// İlgili Entity Tipi (WorkOrder, QuoteRequest, vb.)
        /// </summary>
        public string? RelatedEntityType { get; set; }

        /// <summary>
        /// İlgili Entity ID
        /// </summary>
        public int? RelatedEntityId { get; set; }

        /// <summary>
        /// Ekstra veriler (JSON)
        /// </summary>
        public string? ExtraData { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Bildirimi gönderildi olarak işaretle
        /// </summary>
        public void MarkAsSent()
        {
            Status = NotificationStatus.Sent;
            SentDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Bildirimi başarısız olarak işaretle
        /// </summary>
        public void MarkAsFailed(string errorMessage)
        {
            Status = NotificationStatus.Failed;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Bildirimi okundu olarak işaretle
        /// </summary>
        public void MarkAsRead()
        {
            Status = NotificationStatus.Read;
            ReadDate = DateTime.UtcNow;
        }
    }
}

