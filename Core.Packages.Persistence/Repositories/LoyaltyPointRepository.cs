using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class LoyaltyPointRepository : EfEntityRepository<LoyaltyPoint, BaseDbContext>, ILoyaltyPointRepository
    {
        public LoyaltyPointRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<int> GetTotalPointsByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<LoyaltyPoint>()
                .Where(lp => lp.CustomerId == customerId && lp.Type == LoyaltyPointType.Earned)
                .SumAsync(lp => lp.Points, cancellationToken);
        }

        public async Task<List<LoyaltyPoint>> GetPointsHistoryByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<LoyaltyPoint>()
                .Where(lp => lp.CustomerId == customerId)
                .OrderByDescending(lp => lp.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<LoyaltyPoint>> GetExpiringPointsAsync(DateTime expiryDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<LoyaltyPoint>()
                .Where(lp => lp.ExpiryDate.HasValue && 
                            lp.ExpiryDate <= expiryDate && 
                            lp.Type == LoyaltyPointType.Earned)
                .ToListAsync(cancellationToken);
        }
    }
}
