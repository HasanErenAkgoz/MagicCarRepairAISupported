using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Kullanım takibi entity'si
    /// </summary>
    public class UsageTracking : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Client ID
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Limit türü
        /// </summary>
        public LimitType LimitType { get; set; }

        /// <summary>
        /// Mevcut dönem (ay/yıl formatında: YYYY-MM)
        /// </summary>
        public string CurrentPeriod { get; set; } = string.Empty;

        /// <summary>
        /// Kullanım sayısı
        /// </summary>
        public int UsageCount { get; set; } = 0;

        /// <summary>
        /// Limit miktarı (-1 = sınırsız)
        /// </summary>
        public int LimitAmount { get; set; }

        /// <summary>
        /// Limit sıfırlama tarihi
        /// </summary>
        public DateTime ResetDate { get; set; }

        /// <summary>
        /// Kullanım sayısını artırır
        /// </summary>
        public void IncrementUsage()
        {
            UsageCount++;
        }

        /// <summary>
        /// Limit aşıldı mı kontrol eder
        /// </summary>
        public bool IsLimitExceeded()
        {
            if (LimitAmount == -1) // Sınırsız
            {
                return false;
            }
            return UsageCount >= LimitAmount;
        }

        /// <summary>
        /// Kalan limiti getirir
        /// </summary>
        public int GetRemainingLimit()
        {
            if (LimitAmount == -1) // Sınırsız
            {
                return int.MaxValue;
            }
            return Math.Max(0, LimitAmount - UsageCount);
        }
    }
}
