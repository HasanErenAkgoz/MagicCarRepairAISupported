using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class SalaryPaymentRepository : EfEntityRepository<SalaryPayment, BaseDbContext>, ISalaryPaymentRepository
    {
        public SalaryPaymentRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<SalaryPayment?> GetByIdAsync(int id)
        {
            return await Context.Set<SalaryPayment>()
                .Include(s => s.Employee)
                .Include(s => s.Client)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SalaryPayment>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<SalaryPayment>()
                .Include(s => s.Employee)
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SalaryPayment>> GetByPeriodAsync(int year, int? month, CancellationToken cancellationToken = default)
        {
            var query = Context.Set<SalaryPayment>()
                .Include(s => s.Employee)
                .Where(s => s.Year == year);

            if (month.HasValue)
            {
                query = query.Where(s => s.Month == month.Value);
            }

            return await query
                .OrderBy(s => s.Month)
                .ThenBy(s => s.EmployeeId)
                .ToListAsync(cancellationToken);
        }

        public async Task<SalaryPayment?> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month, CancellationToken cancellationToken = default)
        {
            return await Context.Set<SalaryPayment>()
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.Year == year && s.Month == month, cancellationToken);
        }

        public async Task<decimal> GetTotalSalaryByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<SalaryPayment>()
                .Where(s => s.PaymentDate >= startDate && s.PaymentDate <= endDate)
                .SumAsync(s => s.NetSalary, cancellationToken);
        }
    }
}
