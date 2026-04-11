using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class UserSessionRepository : EfEntityRepository<UserSession, BaseDbContext>, IUserSessionRepository
    {
        public UserSessionRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<UserSession?> GetByTokenIdAsync(string tokenId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<UserSession>()
                .FirstOrDefaultAsync(us => us.TokenId == tokenId, cancellationToken);
        }

        public async Task<List<UserSession>> GetActiveSessionsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<UserSession>()
                .Where(us => us.UserId == userId && us.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(us => us.LastActivityAt)
                .ToListAsync(cancellationToken);
        }

        public async Task InvalidateExpiredSessionsAsync(CancellationToken cancellationToken = default)
        {
            var expiredSessions = await Context.Set<UserSession>()
                .Where(us => us.ExpiresAt <= DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            if (expiredSessions.Any())
            {
                Context.Set<UserSession>().RemoveRange(expiredSessions);
                await SaveChangesAsync();
            }
        }
    }
}
