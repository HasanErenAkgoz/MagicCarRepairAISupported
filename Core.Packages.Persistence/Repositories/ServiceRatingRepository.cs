using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class ServiceRatingRepository : EfEntityRepository<ServiceRating, BaseDbContext>, IServiceRatingRepository
    {
        public ServiceRatingRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<ServiceRating?> GetByIdAsync(int id)
        {
            return await Context.Set<ServiceRating>()
                .Include(r => r.Client)
                .Include(r => r.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ServiceRating?> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServiceRating>()
                .Include(r => r.Customer)
                .Include(r => r.WorkOrder)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(r => r.WorkOrderId == workOrderId, cancellationToken);
        }

        public async Task<List<ServiceRating>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServiceRating>()
                .Include(r => r.Customer)
                .Include(r => r.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                .Where(r => r.ClientId == clientId)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ServiceRating>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServiceRating>()
                .Include(r => r.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                .Include(r => r.Client)
                .Where(r => r.CustomerId == customerId)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ServiceRating>> GetByStatusAsync(RatingStatus status, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServiceRating>()
                .Include(r => r.Customer)
                .Include(r => r.WorkOrder)
                .Include(r => r.Client)
                .Where(r => r.Status == status)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<ServiceRating?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServiceRating>()
                .Include(r => r.Client)
                .Include(r => r.Customer)
                .Include(r => r.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                .Include(r => r.WorkOrder)
                    .ThenInclude(wo => wo.Customer)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<decimal> GetAverageRatingByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
        {
            var ratings = await Context.Set<ServiceRating>()
                .Where(r => r.ClientId == clientId && r.Status == RatingStatus.Approved)
                .ToListAsync(cancellationToken);

            if (!ratings.Any())
                return 0;

            var average = ratings.Average(r => r.CalculateAverageRating());
            return Math.Round(average, 2);
        }

        public async Task<int> GetRatingCountByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServiceRating>()
                .Where(r => r.ClientId == clientId && r.Status == RatingStatus.Approved)
                .CountAsync(cancellationToken);
        }
    }
}
