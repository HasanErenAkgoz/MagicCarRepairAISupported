using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class PartStockRepository : EfEntityRepository<PartStock, BaseDbContext>, IPartStockRepository
    {
        public PartStockRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<PartStock?> GetByIdAsync(int id)
        {
            return await Context.Set<PartStock>().FindAsync(id);
        }

        public async Task<PartStock?> GetByPartIdAsync(int partId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<PartStock>()
                .Include(s => s.Part)
                .FirstOrDefaultAsync(s => s.PartId == partId, cancellationToken);
        }

        public async Task<List<PartStock>> GetLowStockItemsAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<PartStock>()
                .Include(s => s.Part)
                .Where(s => s.Part.IsLowStockAlertEnabled && 
                           s.Quantity <= s.Part.MinimumStockLevel)
                .OrderBy(s => s.Quantity)
                .ToListAsync(cancellationToken);
        }
    }
}

