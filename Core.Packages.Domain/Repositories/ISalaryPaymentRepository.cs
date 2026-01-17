using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface ISalaryPaymentRepository : IEntityRepository<SalaryPayment, int>
    {
        Task<List<SalaryPayment>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<List<SalaryPayment>> GetByPeriodAsync(int year, int? month, CancellationToken cancellationToken = default);
        Task<SalaryPayment?> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalSalaryByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
