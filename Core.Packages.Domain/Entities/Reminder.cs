using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Hatırlatma entity'si
    /// </summary>
    public class Reminder : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Kullanıcı ID (hatırlatma alacak kişi)
        /// </summary>
        public int UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// Hatırlatma başlığı
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Hatırlatma içeriği
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Hatırlatma tarihi
        /// </summary>
        public DateTime ReminderDate { get; set; }

        /// <summary>
        /// Hatırlatma tipi (WorkOrder, Appointment, Payment, Custom)
        /// </summary>
        public ReminderType Type { get; set; }

        /// <summary>
        /// İlgili Entity Tipi
        /// </summary>
        public string? RelatedEntityType { get; set; }

        /// <summary>
        /// İlgili Entity ID
        /// </summary>
        public int? RelatedEntityId { get; set; }

        /// <summary>
        /// Gönderildi mi?
        /// </summary>
        public bool IsSent { get; set; } = false;

        /// <summary>
        /// Gönderim tarihi
        /// </summary>
        public DateTime? SentDate { get; set; }

        /// <summary>
        /// Tekrarlayan hatırlatma mı?
        /// </summary>
        public bool IsRecurring { get; set; } = false;

        /// <summary>
        /// Tekrar periyodu (gün cinsinden)
        /// </summary>
        public int? RecurrenceDays { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }

    public enum ReminderType
    {
        WorkOrder = 1,
        Appointment = 2,
        Payment = 3,
        Maintenance = 4,
        Custom = 5
    }
}
