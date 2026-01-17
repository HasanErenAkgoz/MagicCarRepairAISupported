using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface ICertificateRepository : IEntityRepository<Certificate, int>
    {
        Task<List<Certificate>> GetActiveCertificatesAsync(int clientId, CancellationToken cancellationToken = default);
        Task<List<Certificate>> GetExpiredCertificatesAsync(int clientId, CancellationToken cancellationToken = default);
        Task<List<Certificate>> GetExpiringCertificatesAsync(int clientId, int daysBeforeExpiry, CancellationToken cancellationToken = default);
        Task<List<Certificate>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
    }
}
