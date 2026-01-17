using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class ServicePortfolioRepository : EfEntityRepository<ServicePortfolio, BaseDbContext>, IServicePortfolioRepository
    {
        public ServicePortfolioRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<ServicePortfolio?> GetByIdAsync(int id)
        {
            return await Context.Set<ServicePortfolio>()
                .Include(sp => sp.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                .Include(sp => sp.Client)
                .FirstOrDefaultAsync(sp => sp.Id == id);
        }

        public async Task<List<ServicePortfolio>> GetPublishedPortfoliosAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServicePortfolio>()
                .Include(sp => sp.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                .Where(sp => sp.ClientId == clientId && 
                           sp.IsPublished && 
                           sp.CustomerApprovalStatus == CustomerApprovalStatus.Approved)
                .OrderByDescending(sp => sp.DisplayOrder)
                .ThenByDescending(sp => sp.PublishedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<ServicePortfolio?> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServicePortfolio>()
                .Include(sp => sp.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                .Include(sp => sp.Client)
                .FirstOrDefaultAsync(sp => sp.WorkOrderId == workOrderId, cancellationToken);
        }

        public async Task<List<ServicePortfolio>> GetPendingApprovalsAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServicePortfolio>()
                .Include(sp => sp.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                    .ThenInclude(v => v.Customer)
                .Where(sp => sp.ClientId == clientId && 
                           sp.CustomerApprovalStatus == CustomerApprovalStatus.Pending)
                .OrderByDescending(sp => sp.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ServicePortfolio>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ServicePortfolio>()
                .Include(sp => sp.WorkOrder)
                    .ThenInclude(wo => wo.Vehicle)
                .Where(sp => sp.ClientId == clientId)
                .OrderByDescending(sp => sp.CreatedDate)
                .ToListAsync(cancellationToken);
        }
    }
}
