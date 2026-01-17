using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class IncomeRepository : EfEntityRepository<Income, BaseDbContext>, IIncomeRepository
    {
        public IncomeRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Income?> GetByIdAsync(int id)
        {
            return await Context.Set<Income>().FindAsync(id);
        }

        public async Task<List<Income>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Income>()
                .Where(i => i.TransactionDate >= startDate && i.TransactionDate <= endDate)
                .OrderByDescending(i => i.TransactionDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Income>> GetByIncomeTypeAsync(IncomeType incomeType, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Income>()
                .Where(i => i.IncomeType == incomeType)
                .OrderByDescending(i => i.TransactionDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Income>> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Income>()
                .Where(i => i.WorkOrderId == workOrderId)
                .OrderByDescending(i => i.TransactionDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<decimal> GetTotalIncomeByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Income>()
                .Where(i => i.TransactionDate >= startDate && i.TransactionDate <= endDate)
                .SumAsync(i => i.Amount, cancellationToken);
        }
    }
}

