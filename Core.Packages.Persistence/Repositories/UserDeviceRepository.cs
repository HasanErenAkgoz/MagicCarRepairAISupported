using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class UserDeviceRepository : EfEntityRepository<UserDevice, BaseDbContext>, IUserDeviceRepository
    {
        public UserDeviceRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<UserDevice?> GetByUserIdAndDeviceIdAsync(int userId, string deviceId, CancellationToken cancellationToken = default)
        {
            return await Context.UserDevices
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(ud => ud.UserId == userId && ud.DeviceId == deviceId, cancellationToken);
        }

        public async Task UpsertLoginDeviceAsync(UserDevice device, CancellationToken cancellationToken = default)
        {
            var existing = await Context.UserDevices
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    ud => ud.UserId == device.UserId && ud.DeviceId == device.DeviceId,
                    cancellationToken);

            if (existing == null)
            {
                await Context.UserDevices.AddAsync(device, cancellationToken);
            }
            else
            {
                existing.LastLoginAt = device.LastLoginAt;
                existing.IsTrusted = device.IsTrusted;
                if (!string.IsNullOrEmpty(device.DeviceName))
                    existing.DeviceName = device.DeviceName;
            }

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
