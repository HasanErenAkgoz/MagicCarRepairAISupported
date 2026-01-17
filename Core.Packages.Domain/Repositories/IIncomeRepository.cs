using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IIncomeRepository : IEntityRepository<Income, int>
    {
        Task<List<Income>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<List<Income>> GetByIncomeTypeAsync(IncomeType incomeType, CancellationToken cancellationToken = default);
        Task<List<Income>> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalIncomeByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}

