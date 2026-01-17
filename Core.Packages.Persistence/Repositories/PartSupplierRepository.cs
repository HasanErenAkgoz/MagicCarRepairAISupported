using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class PartSupplierRepository : EfEntityRepository<PartSupplier, BaseDbContext>, IPartSupplierRepository
    {
        public PartSupplierRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<PartSupplier?> GetByIdAsync(int id)
        {
            return await Context.Set<PartSupplier>().FindAsync(id);
        }

        public async Task<PartSupplier?> GetByCompanyNameAsync(string companyName, CancellationToken cancellationToken = default)
        {
            return await Context.Set<PartSupplier>()
                .FirstOrDefaultAsync(s => s.CompanyName == companyName, cancellationToken);
        }

        public async Task<List<PartSupplier>> GetActiveSuppliersAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<PartSupplier>()
                .Where(s => s.IsActive)
                .OrderBy(s => s.CompanyName)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsCompanyNameExistsAsync(string companyName, CancellationToken cancellationToken = default)
        {
            return await Context.Set<PartSupplier>()
                .AnyAsync(s => s.CompanyName == companyName, cancellationToken);
        }
    }
}

