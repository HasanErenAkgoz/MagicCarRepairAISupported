using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class TaxRepository : EfEntityRepository<Tax, BaseDbContext>, ITaxRepository
    {
        public TaxRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Tax?> GetByIdAsync(int id)
        {
            return await Context.Set<Tax>()
                .Include(t => t.Client)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tax>> GetByTaxTypeAsync(TaxType taxType, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Tax>()
                .Where(t => t.TaxType == taxType)
                .OrderByDescending(t => t.DueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Tax>> GetByStatusAsync(TaxStatus status, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Tax>()
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.DueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Tax>> GetByPeriodAsync(int year, int? month, CancellationToken cancellationToken = default)
        {
            var query = Context.Set<Tax>()
                .Where(t => t.Year == year);

            if (month.HasValue)
            {
                query = query.Where(t => t.Month == month.Value);
            }

            return await query
                .OrderBy(t => t.Month)
                .ThenBy(t => t.DueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Tax>> GetOverdueTaxesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            return await Context.Set<Tax>()
                .Where(t => t.Status == TaxStatus.Pending && t.DueDate < now)
                .OrderBy(t => t.DueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Tax>> GetByDueDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Tax>()
                .Where(t => t.DueDate >= startDate && t.DueDate <= endDate)
                .OrderBy(t => t.DueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<decimal> GetTotalTaxByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Tax>()
                .Where(t => t.PaymentDate.HasValue && t.PaymentDate.Value >= startDate && t.PaymentDate.Value <= endDate)
                .SumAsync(t => t.Amount, cancellationToken);
        }
    }
}
