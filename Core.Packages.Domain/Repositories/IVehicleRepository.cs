using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IVehicleRepository : IEntityRepository<Vehicle, int>
    {
        Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default);
        Task<List<Vehicle>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
        Task<bool> IsLicensePlateExistsAsync(string licensePlate, CancellationToken cancellationToken = default);
    }
}

