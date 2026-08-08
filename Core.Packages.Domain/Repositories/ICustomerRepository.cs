using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface ICustomerRepository : IEntityRepository<Customer, int>
    {
        Task<Customer?> GetByIdForTenantAsync(int id, int clientId, CancellationToken cancellationToken = default);

        Task<Customer?> GetByIdWithVehiclesAsync(int id, CancellationToken cancellationToken = default);

        Task<Customer?> GetByUserIdForTenantAsync(int userId, int clientId, CancellationToken cancellationToken = default);

        Task<Customer?> GetByIdentityNoAsync(string identityNo, CancellationToken cancellationToken = default);
        Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsIdentityNoExistsAsync(string identityNo, CancellationToken cancellationToken = default);
        Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default);
    }
}

