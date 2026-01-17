using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IFacilityPhotoRepository : IEntityRepository<FacilityPhoto, int>
    {
        Task<List<FacilityPhoto>> GetPublishedPhotosAsync(int clientId, CancellationToken cancellationToken = default);
        Task<List<FacilityPhoto>> GetByCategoryAsync(int clientId, string category, CancellationToken cancellationToken = default);
        Task<List<FacilityPhoto>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
    }
}
