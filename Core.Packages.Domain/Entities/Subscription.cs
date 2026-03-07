using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Abonelik entity'si
    /// </summary>
    public class Subscription : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Client ID (FK)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Abonelik planı
        /// </summary>
        public SubscriptionPlan Plan { get; set; }

        /// <summary>
        /// Abonelik durumu
        /// </summary>
        public new SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;

        /// <summary>
        /// Başlangıç tarihi
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Bitiş tarihi
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Aylık fiyat
        /// </summary>
        public decimal MonthlyPrice { get; set; }

        /// <summary>
        /// Yıllık fiyat (yıllık ödeme için)
        /// </summary>
        public decimal? YearlyPrice { get; set; }

        /// <summary>
        /// Maksimum iş emri sayısı (-1 = sınırsız)
        /// </summary>
        public int MaxWorkOrders { get; set; }

        /// <summary>
        /// Maksimum kullanıcı sayısı (-1 = sınırsız)
        /// </summary>
        public int MaxUsers { get; set; }

        /// <summary>
        /// Maksimum AI analiz sayısı/ay (-1 = sınırsız)
        /// </summary>
        public int MaxAIAnalyses { get; set; }

        /// <summary>
        /// Aktif özellikler (JSON formatında: ["AdvancedReports", "APIAccess", ...])
        /// </summary>
        public string? Features { get; set; }

        /// <summary>
        /// Otomatik yenileme aktif mi?
        /// </summary>
        public bool AutoRenew { get; set; } = true;

        /// <summary>
        /// İptal tarihi
        /// </summary>
        public DateTime? CancelledDate { get; set; }

        /// <summary>
        /// İptal nedeni
        /// </summary>
        public string? CancellationReason { get; set; }

        /// <summary>
        /// Son ödeme tarihi
        /// </summary>
        public DateTime? LastPaymentDate { get; set; }

        /// <summary>
        /// Sonraki ödeme tarihi
        /// </summary>
        public DateTime? NextPaymentDate { get; set; }

        // Navigation properties
        public virtual ICollection<SubscriptionPayment> Payments { get; set; } = new List<SubscriptionPayment>();

        /// <summary>
        /// Aboneliğin aktif olup olmadığını kontrol eder
        /// </summary>
        public bool IsActive()
        {
            return Status == SubscriptionStatus.Active && 
                   DateTime.UtcNow >= StartDate && 
                   DateTime.UtcNow <= EndDate;
        }

        /// <summary>
        /// Aboneliğin süresi dolmuş mu kontrol eder
        /// </summary>
        public bool IsExpired()
        {
            return DateTime.UtcNow > EndDate;
        }
    }
}
