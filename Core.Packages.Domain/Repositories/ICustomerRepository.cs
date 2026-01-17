using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface ICustomerRepository : IEntityRepository<Customer, int>
    {
        Task<Customer?> GetByIdentityNoAsync(string identityNo, CancellationToken cancellationToken = default);
        Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsIdentityNoExistsAsync(string identityNo, CancellationToken cancellationToken = default);
        Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default);
    }
}

