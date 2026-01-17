using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IExpenseRepository : IEntityRepository<Expense, int>
    {
        Task<List<Expense>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<List<Expense>> GetByExpenseTypeAsync(ExpenseType expenseType, CancellationToken cancellationToken = default);
        Task<List<Expense>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalExpenseByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}

