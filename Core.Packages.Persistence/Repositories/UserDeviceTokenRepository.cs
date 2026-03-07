using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class UserDeviceTokenRepository : EfEntityRepository<UserDeviceToken, BaseDbContext>, IUserDeviceTokenRepository
    {
        public UserDeviceTokenRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<List<UserDeviceToken>> GetActiveTokensByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<UserDeviceToken>()
                .Where(t => t.UserId == userId && t.IsActive)
                .ToListAsync(cancellationToken);
        }

        public async Task<UserDeviceToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await Context.Set<UserDeviceToken>()
                .FirstOrDefaultAsync(t => t.Token == token, cancellationToken);
        }

        public async Task DeleteAllUserTokensAsync(int userId, CancellationToken cancellationToken = default)
        {
            var tokens = await Context.Set<UserDeviceToken>()
                .Where(t => t.UserId == userId)
                .ToListAsync(cancellationToken);

            Context.Set<UserDeviceToken>().RemoveRange(tokens);
        }
    }
}
