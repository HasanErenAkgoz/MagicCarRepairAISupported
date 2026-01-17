using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IClientRepository : IEntityRepository<Client, int>
    {
        Task<Client?> GetByCodeAsync(string code);
        Task<bool> IsCodeUniqueAsync(string code, int? excludeId = null);
    }
}

