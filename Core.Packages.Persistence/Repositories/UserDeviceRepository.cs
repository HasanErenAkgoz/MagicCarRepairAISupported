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
            return await Context.Set<UserDevice>()
                .FirstOrDefaultAsync(ud => ud.UserId == userId && ud.DeviceId == deviceId, cancellationToken);
        }
    }
}
