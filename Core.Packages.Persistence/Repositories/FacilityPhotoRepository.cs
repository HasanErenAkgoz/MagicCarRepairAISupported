using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class FacilityPhotoRepository : EfEntityRepository<FacilityPhoto, BaseDbContext>, IFacilityPhotoRepository
    {
        public FacilityPhotoRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<FacilityPhoto?> GetByIdAsync(int id)
        {
            return await Context.Set<FacilityPhoto>()
                .Include(f => f.Client)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<FacilityPhoto>> GetPublishedPhotosAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<FacilityPhoto>()
                .Where(f => f.ClientId == clientId && f.IsPublic)
                .OrderBy(f => f.DisplayOrder)
                .ThenByDescending(f => f.UploadDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FacilityPhoto>> GetByCategoryAsync(int clientId, string category, CancellationToken cancellationToken = default)
        {
            return await Context.Set<FacilityPhoto>()
                .Where(f => f.ClientId == clientId && 
                          f.IsPublic && 
                          f.Category == category)
                .OrderBy(f => f.DisplayOrder)
                .ThenByDescending(f => f.UploadDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FacilityPhoto>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<FacilityPhoto>()
                .Where(f => f.ClientId == clientId)
                .OrderBy(f => f.DisplayOrder)
                .ThenByDescending(f => f.UploadDate)
                .ToListAsync(cancellationToken);
        }
    }
}
