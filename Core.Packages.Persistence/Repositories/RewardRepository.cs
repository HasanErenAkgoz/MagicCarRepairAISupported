using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class RewardRepository : EfEntityRepository<Reward, BaseDbContext>, IRewardRepository
    {
        public RewardRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<List<Reward>> GetActiveRewardsAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            return await Context.Set<Reward>()
                .Where(r => r.IsActive &&
                           (r.ValidFrom == null || r.ValidFrom <= now) &&
                           (r.ValidTo == null || r.ValidTo >= now) &&
                           (r.StockQuantity == null || r.UsedQuantity < r.StockQuantity))
                .OrderBy(r => r.RequiredPoints)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Reward>> GetAvailableRewardsByPointsAsync(int points, CancellationToken cancellationToken = default)
        {
            var activeRewards = await GetActiveRewardsAsync(cancellationToken);
            return activeRewards
                .Where(r => r.RequiredPoints <= points)
                .ToList();
        }
    }
}
