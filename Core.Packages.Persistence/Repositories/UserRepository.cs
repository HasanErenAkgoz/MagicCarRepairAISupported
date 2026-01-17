using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class UserRepository : EfEntityRepository<User, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await Context.Set<User>().FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await Context.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetByIdentityNoAsync(string identityNo, CancellationToken cancellationToken = default)
        {
            return await Context.Set<User>()
                .FirstOrDefaultAsync(u => u.IdentityNo == identityNo, cancellationToken);
        }

        public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            return await Context.Set<User>()
                .AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> IsIdentityNoExistsAsync(string identityNo, CancellationToken cancellationToken = default)
        {
            return await Context.Set<User>()
                .AnyAsync(u => u.IdentityNo == identityNo, cancellationToken);
        }
    }
}
