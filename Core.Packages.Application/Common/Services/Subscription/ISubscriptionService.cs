using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using SubscriptionEntity = MagicCarRepairAISupported.Domain.Entities.Subscription;

namespace MagicCarRepairAISupported.Application.Common.Services.Subscription
{
    /// <summary>
    /// Abonelik servisi
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Client'ın aktif aboneliğini getirir
        /// </summary>
        Task<SubscriptionEntity?> GetCurrentSubscriptionAsync(int clientId);

        /// <summary>
        /// Belirli bir özelliğe erişim kontrolü yapar
        /// </summary>
        Task<bool> CheckFeatureAccessAsync(int clientId, string featureName);

        /// <summary>
        /// Limit kontrolü yapar
        /// </summary>
        Task<bool> CheckLimitAsync(int clientId, LimitType limitType, int currentUsage);

        /// <summary>
        /// Kalan limiti getirir
        /// </summary>
        Task<int> GetRemainingLimitAsync(int clientId, LimitType limitType);

        /// <summary>
        /// Plan limitlerini getirir
        /// </summary>
        Task<SubscriptionLimits> GetPlanLimitsAsync(int clientId);

        /// <summary>
        /// Aboneliğin aktif olup olmadığını kontrol eder
        /// </summary>
        Task<bool> IsSubscriptionActiveAsync(int clientId);
    }

    /// <summary>
    /// Limit türleri
    /// </summary>
    public enum LimitType
    {
        WorkOrder = 1,
        User = 2,
        AIAnalysis = 3
    }

    /// <summary>
    /// Abonelik limitleri
    /// </summary>
    public class SubscriptionLimits
    {
        public int MaxWorkOrders { get; set; }
        public int MaxUsers { get; set; }
        public int MaxAIAnalyses { get; set; }
        public List<string> Features { get; set; } = new List<string>();
    }
}
