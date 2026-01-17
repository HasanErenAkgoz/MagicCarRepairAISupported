using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class VehicleRepository : EfEntityRepository<Vehicle, BaseDbContext>, IVehicleRepository
    {
        public VehicleRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await Context.Set<Vehicle>().FindAsync(id);
        }

        public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Vehicle>()
                .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate, cancellationToken);
        }

        public async Task<List<Vehicle>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Vehicle>()
                .Where(v => v.CustomerId == customerId)
                .OrderBy(v => v.Brand)
                .ThenBy(v => v.Model)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsLicensePlateExistsAsync(string licensePlate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Vehicle>()
                .AnyAsync(v => v.LicensePlate == licensePlate, cancellationToken);
        }
    }
}

