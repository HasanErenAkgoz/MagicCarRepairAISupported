using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface ITaxRepository : IEntityRepository<Tax, int>
    {
        Task<List<Tax>> GetByTaxTypeAsync(TaxType taxType, CancellationToken cancellationToken = default);
        Task<List<Tax>> GetByStatusAsync(TaxStatus status, CancellationToken cancellationToken = default);
        Task<List<Tax>> GetByPeriodAsync(int year, int? month, CancellationToken cancellationToken = default);
        Task<List<Tax>> GetOverdueTaxesAsync(CancellationToken cancellationToken = default);
        Task<List<Tax>> GetByDueDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalTaxByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
