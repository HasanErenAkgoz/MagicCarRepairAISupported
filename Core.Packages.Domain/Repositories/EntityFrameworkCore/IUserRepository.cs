using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore
{
    public interface IUserRepository : IEntityRepository<User, int>
    {
        /// <summary>
        /// Login/auth — ignores tenant query filter so a stale X-Client-Id cannot hide the user.
        /// </summary>
        Task<User?> FindByEmailForAuthAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates refresh token without Identity ConcurrencyStamp / tenant filter conflicts.
        /// </summary>
        Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime refreshTokenExpiryTime, CancellationToken cancellationToken = default);
    }
}
