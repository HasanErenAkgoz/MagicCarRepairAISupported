using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore
{
    public interface IUserRepository : IEntityRepository<User, int>
    {
    }
}
