using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class AuditLogRepository : EfEntityRepository<AuditLog, BaseDbContext>, IAuditLogRepository
    {
        public AuditLogRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<List<AuditLog>> GetByEntityAsync(string entityName, int entityId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<AuditLog>()
                .Where(a => a.EntityName == entityName && a.EntityId == entityId)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<AuditLog>> GetByUserIdAsync(int? userId, CancellationToken cancellationToken = default)
        {
            var query = Context.Set<AuditLog>().AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(a => a.UserId == userId.Value);
            }

            return await query
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<AuditLog>> GetByActionAsync(string action, CancellationToken cancellationToken = default)
        {
            return await Context.Set<AuditLog>()
                .Where(a => a.Action == action)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<AuditLog>()
                .Where(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync(cancellationToken);
        }
    }
}
