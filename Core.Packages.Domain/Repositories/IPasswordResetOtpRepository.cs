using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IPasswordResetOtpRepository : IEntityRepository<PasswordResetOtp, int>
    {
        Task<PasswordResetOtp?> GetLatestOtpForUserAsync(int userId, string phoneNumber, CancellationToken cancellationToken = default);
        Task<PasswordResetOtp?> GetByResetTokenAsync(string resetToken, CancellationToken cancellationToken = default);
        Task InvalidateOldOtpsAsync(int userId, CancellationToken cancellationToken = default);
        Task CleanupExpiredOtpsAsync(CancellationToken cancellationToken = default);
    }
}
