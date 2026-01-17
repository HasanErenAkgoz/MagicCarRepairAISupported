using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IStockMovementRepository : IEntityRepository<StockMovement, int>
    {
        Task<List<StockMovement>> GetByPartIdAsync(int partId, CancellationToken cancellationToken = default);
        Task<List<StockMovement>> GetByMovementTypeAsync(StockMovementType movementType, CancellationToken cancellationToken = default);
        Task<List<StockMovement>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<List<StockMovement>> GetMovementsByDateRangeAsync(int partId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<List<StockMovement>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    }
}

