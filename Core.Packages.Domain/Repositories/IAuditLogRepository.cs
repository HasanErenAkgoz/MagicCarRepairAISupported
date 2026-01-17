using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IAuditLogRepository : IEntityRepository<AuditLog, int>
    {
        Task<List<AuditLog>> GetByEntityAsync(string entityName, int entityId, CancellationToken cancellationToken = default);
        Task<List<AuditLog>> GetByUserIdAsync(int? userId, CancellationToken cancellationToken = default);
        Task<List<AuditLog>> GetByActionAsync(string action, CancellationToken cancellationToken = default);
        Task<List<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
