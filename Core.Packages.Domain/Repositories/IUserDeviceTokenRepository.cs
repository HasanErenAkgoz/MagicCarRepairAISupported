using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    /// <summary>
    /// User device token repository interface
    /// </summary>
    public interface IUserDeviceTokenRepository : IEntityRepository<UserDeviceToken, int>
    {
        /// <summary>
        /// Kullanıcının aktif token'larını getirir
        /// </summary>
        Task<List<UserDeviceToken>> GetActiveTokensByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Token'a göre device token'ı getirir
        /// </summary>
        Task<UserDeviceToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kullanıcının tüm token'larını siler (logout veya token refresh için)
        /// </summary>
        Task DeleteAllUserTokensAsync(int userId, CancellationToken cancellationToken = default);
    }
}
