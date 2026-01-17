using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IServicePortfolioRepository : IEntityRepository<ServicePortfolio, int>
    {
        Task<List<ServicePortfolio>> GetPublishedPortfoliosAsync(int clientId, CancellationToken cancellationToken = default);
        Task<ServicePortfolio?> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default);
        Task<List<ServicePortfolio>> GetPendingApprovalsAsync(int clientId, CancellationToken cancellationToken = default);
        Task<List<ServicePortfolio>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
    }
}
