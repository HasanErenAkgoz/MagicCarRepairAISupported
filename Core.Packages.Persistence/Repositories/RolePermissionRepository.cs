using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class RolePermissionRepository : EfEntityRepository<RolePermission, BaseDbContext>, IRolePermissionRepository
    {
        public RolePermissionRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
