using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class UploadedFileRepository : EfEntityRepository<UploadedFile, BaseDbContext>, IUploadedFileRepository
    {
        public UploadedFileRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
