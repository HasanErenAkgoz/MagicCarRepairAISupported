using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IServiceRatingRepository : IEntityRepository<ServiceRating, int>
    {
        Task<ServiceRating?> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default);
        Task<List<ServiceRating>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
        Task<List<ServiceRating>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
        Task<List<ServiceRating>> GetByStatusAsync(RatingStatus status, CancellationToken cancellationToken = default);
        Task<ServiceRating?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default);
        Task<decimal> GetAverageRatingByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
        Task<int> GetRatingCountByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
    }
}
