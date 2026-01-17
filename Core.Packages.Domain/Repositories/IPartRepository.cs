using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IPartRepository : IEntityRepository<Part, int>
    {
        Task<Part?> GetByPartCodeAsync(string partCode, CancellationToken cancellationToken = default);
        Task<List<Part>> GetByCategoryAsync(PartCategory category, CancellationToken cancellationToken = default);
        Task<List<Part>> GetLowStockPartsAsync(CancellationToken cancellationToken = default);
        Task<List<Part>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<bool> IsPartCodeExistsAsync(string partCode, CancellationToken cancellationToken = default);
        Task<Part?> GetWithStockAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> IsPartUsedInActiveWorkOrdersAsync(int partId, CancellationToken cancellationToken = default);
    }
}

