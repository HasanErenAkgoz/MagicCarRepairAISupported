using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class PasswordResetOtpRepository : EfEntityRepository<PasswordResetOtp, BaseDbContext>, IPasswordResetOtpRepository
    {
        public PasswordResetOtpRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<PasswordResetOtp?> GetLatestOtpForUserAsync(int userId, string phoneNumber, CancellationToken cancellationToken = default)
        {
            return await Context.Set<PasswordResetOtp>()
                .Where(o => o.UserId == userId && o.PhoneNumber == phoneNumber)
                .OrderByDescending(o => o.CreatedDate)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PasswordResetOtp?> GetByResetTokenAsync(string resetToken, CancellationToken cancellationToken = default)
        {
            return await Context.Set<PasswordResetOtp>()
                .Where(o => o.ResetToken == resetToken)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InvalidateOldOtpsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var oldOtps = await Context.Set<PasswordResetOtp>()
                .Where(o => o.UserId == userId && !o.Used && o.ExpiresAt > DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            foreach (var otp in oldOtps)
            {
                otp.Used = true;
            }

            if (oldOtps.Any())
            {
                Context.Set<PasswordResetOtp>().UpdateRange(oldOtps);
                await SaveChangesAsync();
            }
        }

        public async Task CleanupExpiredOtpsAsync(CancellationToken cancellationToken = default)
        {
            var expiredOtps = await Context.Set<PasswordResetOtp>()
                .Where(o => o.ExpiresAt < DateTime.UtcNow.AddDays(-1)) // 1 günden eski OTP'leri sil
                .ToListAsync(cancellationToken);

            if (expiredOtps.Any())
            {
                Context.Set<PasswordResetOtp>().RemoveRange(expiredOtps);
                await SaveChangesAsync();
            }
        }
    }
}
