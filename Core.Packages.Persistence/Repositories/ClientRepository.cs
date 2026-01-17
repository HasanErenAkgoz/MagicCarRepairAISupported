using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class ClientRepository : EfEntityRepository<Client, BaseDbContext>, IClientRepository
    {
        public ClientRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await Context.Clients.FindAsync(id);
        }

        public async Task<Client?> GetByCodeAsync(string code)
        {
            return await Context.Clients
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<bool> IsCodeUniqueAsync(string code, int? excludeId = null)
        {
            if (excludeId.HasValue)
            {
                return !await Context.Clients
                    .AnyAsync(c => c.Code == code && c.Id != excludeId.Value);
            }
            
            return !await Context.Clients.AnyAsync(c => c.Code == code);
        }
    }
}

