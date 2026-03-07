using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    /// <summary>
    /// Loyalty point repository interface
    /// </summary>
    public interface ILoyaltyPointRepository : IEntityRepository<LoyaltyPoint, int>
    {
        /// <summary>
        /// Müşterinin toplam puanını getirir
        /// </summary>
        Task<int> GetTotalPointsByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Müşterinin puan geçmişini getirir
        /// </summary>
        Task<List<LoyaltyPoint>> GetPointsHistoryByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Süresi dolacak puanları getirir
        /// </summary>
        Task<List<LoyaltyPoint>> GetExpiringPointsAsync(DateTime expiryDate, CancellationToken cancellationToken = default);
    }
}
