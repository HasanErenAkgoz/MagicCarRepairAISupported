using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class ExpenseRepository : EfEntityRepository<Expense, BaseDbContext>, IExpenseRepository
    {
        public ExpenseRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await Context.Set<Expense>().FindAsync(id);
        }

        public async Task<List<Expense>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Expense>()
                .Where(e => e.TransactionDate >= startDate && e.TransactionDate <= endDate)
                .OrderByDescending(e => e.TransactionDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Expense>> GetByExpenseTypeAsync(ExpenseType expenseType, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Expense>()
                .Where(e => e.ExpenseType == expenseType)
                .OrderByDescending(e => e.TransactionDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Expense>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Expense>()
                .Where(e => e.EmployeeId == employeeId)
                .OrderByDescending(e => e.TransactionDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<decimal> GetTotalExpenseByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Expense>()
                .Where(e => e.TransactionDate >= startDate && e.TransactionDate <= endDate)
                .SumAsync(e => e.Amount, cancellationToken);
        }
    }
}

