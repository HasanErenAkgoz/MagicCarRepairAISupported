using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IPartStockRepository : IEntityRepository<PartStock, int>
    {
        Task<PartStock?> GetByPartIdAsync(int partId, CancellationToken cancellationToken = default);
        Task<List<PartStock>> GetLowStockItemsAsync(CancellationToken cancellationToken = default);
    }
}

