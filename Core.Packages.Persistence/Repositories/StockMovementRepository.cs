using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class StockMovementRepository : EfEntityRepository<StockMovement, BaseDbContext>, IStockMovementRepository
    {
        public StockMovementRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<List<StockMovement>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<StockMovement>()
                .Include(m => m.Part)
                .Include(m => m.Employee)
                .OrderByDescending(m => m.MovementDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<StockMovement?> GetByIdAsync(int id)
        {
            return await Context.Set<StockMovement>().FindAsync(id);
        }

        public async Task<List<StockMovement>> GetByPartIdAsync(int partId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<StockMovement>()
                .Include(m => m.Part)
                .Include(m => m.Employee)
                .Where(m => m.PartId == partId)
                .OrderByDescending(m => m.MovementDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<StockMovement>> GetByMovementTypeAsync(StockMovementType movementType, CancellationToken cancellationToken = default)
        {
            return await Context.Set<StockMovement>()
                .Include(m => m.Part)
                .Include(m => m.Employee)
                .Where(m => m.MovementType == movementType)
                .OrderByDescending(m => m.MovementDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<StockMovement>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<StockMovement>()
                .Include(m => m.Part)
                .Include(m => m.Employee)
                .Where(m => m.MovementDate >= startDate && m.MovementDate <= endDate)
                .OrderByDescending(m => m.MovementDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<StockMovement>> GetMovementsByDateRangeAsync(int partId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<StockMovement>()
                .Include(m => m.Part)
                .Include(m => m.Employee)
                .Where(m => m.PartId == partId && m.MovementDate >= startDate && m.MovementDate <= endDate)
                .OrderBy(m => m.MovementDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<StockMovement>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<StockMovement>()
                .Include(m => m.Part)
                .Include(m => m.Employee)
                .Where(m => m.EmployeeId == employeeId)
                .OrderByDescending(m => m.MovementDate)
                .ToListAsync(cancellationToken);
        }
    }
}

