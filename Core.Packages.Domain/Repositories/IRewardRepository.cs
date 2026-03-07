using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    /// <summary>
    /// Reward repository interface
    /// </summary>
    public interface IRewardRepository : IEntityRepository<Reward, int>
    {
        /// <summary>
        /// Aktif ödülleri getirir
        /// </summary>
        Task<List<Reward>> GetActiveRewardsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Müşterinin puanına uygun ödülleri getirir
        /// </summary>
        Task<List<Reward>> GetAvailableRewardsByPointsAsync(int points, CancellationToken cancellationToken = default);
    }
}
