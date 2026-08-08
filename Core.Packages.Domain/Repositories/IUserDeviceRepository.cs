using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IUserDeviceRepository : IEntityRepository<UserDevice, int>
    {
        Task<UserDevice?> GetByUserIdAndDeviceIdAsync(int userId, string deviceId, CancellationToken cancellationToken = default);

        Task UpsertLoginDeviceAsync(UserDevice device, CancellationToken cancellationToken = default);
    }
}
