using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IUserSessionRepository : IEntityRepository<UserSession, int>
    {
        Task<UserSession?> GetByTokenIdAsync(string tokenId, CancellationToken cancellationToken = default);
        Task<List<UserSession>> GetActiveSessionsByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task InvalidateExpiredSessionsAsync(CancellationToken cancellationToken = default);
    }
}
