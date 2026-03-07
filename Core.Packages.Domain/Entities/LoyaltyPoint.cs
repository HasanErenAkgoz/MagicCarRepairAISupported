using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Müşteri sadakat puanı entity'si
    /// </summary>
    public class LoyaltyPoint : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Puan miktarı (pozitif: kazanılan, negatif: kullanılan)
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Puan tipi (Earned, Redeemed, Expired)
        /// </summary>
        public LoyaltyPointType Type { get; set; }

        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// İlgili iş emri ID (puan kazanıldıysa)
        /// </summary>
        public int? WorkOrderId { get; set; }
        public virtual WorkOrder? WorkOrder { get; set; }

        /// <summary>
        /// İlgili reward ID (puan kullanıldıysa)
        /// </summary>
        public int? RewardId { get; set; }
        public virtual Reward? Reward { get; set; }

        /// <summary>
        /// Son kullanma tarihi
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }

    public enum LoyaltyPointType
    {
        Earned = 1,    // Kazanıldı
        Redeemed = 2,  // Kullanıldı
        Expired = 3    // Süresi doldu
    }
}
