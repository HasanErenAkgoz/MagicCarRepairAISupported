using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface ISubscriptionRepository : IEntityRepository<Subscription, int>
    {
        /// <summary>
        /// Client'ın aktif aboneliğini getirir
        /// </summary>
        Task<Subscription?> GetActiveSubscriptionAsync(int clientId);

        /// <summary>
        /// Client'ın tüm aboneliklerini getirir
        /// </summary>
        Task<List<Subscription>> GetSubscriptionsByClientIdAsync(int clientId);

        /// <summary>
        /// Belirli bir plana sahip aktif abonelikleri getirir
        /// </summary>
        Task<List<Subscription>> GetActiveSubscriptionsByPlanAsync(SubscriptionPlan plan);

        /// <summary>
        /// Süresi dolacak abonelikleri getirir
        /// </summary>
        Task<List<Subscription>> GetExpiringSubscriptionsAsync(DateTime expiryDate);
    }
}
