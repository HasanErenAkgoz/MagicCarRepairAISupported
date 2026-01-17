using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IPartSupplierRepository : IEntityRepository<PartSupplier, int>
    {
        Task<PartSupplier?> GetByCompanyNameAsync(string companyName, CancellationToken cancellationToken = default);
        Task<List<PartSupplier>> GetActiveSuppliersAsync(CancellationToken cancellationToken = default);
        Task<bool> IsCompanyNameExistsAsync(string companyName, CancellationToken cancellationToken = default);
    }
}

