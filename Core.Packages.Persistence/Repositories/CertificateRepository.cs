using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class CertificateRepository : EfEntityRepository<Certificate, BaseDbContext>, ICertificateRepository
    {
        public CertificateRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Certificate?> GetByIdAsync(int id)
        {
            return await Context.Set<Certificate>()
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Certificate>> GetActiveCertificatesAsync(int clientId, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            return await Context.Set<Certificate>()
                .Where(c => c.ClientId == clientId && 
                          c.IsPublic &&
                          (!c.ExpiryDate.HasValue || c.ExpiryDate.Value >= now))
                .OrderBy(c => c.DisplayOrder)
                .ThenByDescending(c => c.IssueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Certificate>> GetExpiredCertificatesAsync(int clientId, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            return await Context.Set<Certificate>()
                .Where(c => c.ClientId == clientId && 
                          c.ExpiryDate.HasValue && 
                          c.ExpiryDate.Value < now)
                .OrderByDescending(c => c.ExpiryDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Certificate>> GetExpiringCertificatesAsync(int clientId, int daysBeforeExpiry, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var expiryDate = now.AddDays(daysBeforeExpiry);
            return await Context.Set<Certificate>()
                .Where(c => c.ClientId == clientId && 
                          c.ExpiryDate.HasValue && 
                          c.ExpiryDate.Value >= now && 
                          c.ExpiryDate.Value <= expiryDate)
                .OrderBy(c => c.ExpiryDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Certificate>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Certificate>()
                .Where(c => c.ClientId == clientId)
                .OrderBy(c => c.DisplayOrder)
                .ThenByDescending(c => c.IssueDate)
                .ToListAsync(cancellationToken);
        }
    }
}
