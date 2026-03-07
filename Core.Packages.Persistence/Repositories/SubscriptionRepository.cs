using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class SubscriptionRepository : EfEntityRepository<Subscription, BaseDbContext>, ISubscriptionRepository
    {
        public SubscriptionRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Subscription?> GetActiveSubscriptionAsync(int clientId)
        {
            return await Context.Subscriptions
                .Include(s => s.Client)
                .Include(s => s.Payments)
                .Where(s => s.ClientId == clientId && 
                           s.Status == SubscriptionStatus.Active &&
                           s.StartDate <= DateTime.UtcNow &&
                           s.EndDate >= DateTime.UtcNow)
                .OrderByDescending(s => s.StartDate)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Subscription>> GetSubscriptionsByClientIdAsync(int clientId)
        {
            return await Context.Subscriptions
                .Include(s => s.Client)
                .Include(s => s.Payments)
                .Where(s => s.ClientId == clientId)
                .OrderByDescending(s => s.StartDate)
                .ToListAsync();
        }

        public async Task<List<Subscription>> GetActiveSubscriptionsByPlanAsync(SubscriptionPlan plan)
        {
            return await Context.Subscriptions
                .Include(s => s.Client)
                .Where(s => s.Plan == plan &&
                           s.Status == SubscriptionStatus.Active &&
                           s.StartDate <= DateTime.UtcNow &&
                           s.EndDate >= DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<List<Subscription>> GetExpiringSubscriptionsAsync(DateTime expiryDate)
        {
            return await Context.Subscriptions
                .Include(s => s.Client)
                .Where(s => s.Status == SubscriptionStatus.Active &&
                           s.EndDate <= expiryDate &&
                           s.EndDate >= DateTime.UtcNow)
                .ToListAsync();
        }
    }
}
