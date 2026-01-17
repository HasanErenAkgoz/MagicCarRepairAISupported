using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class CustomerRepository : EfEntityRepository<Customer, BaseDbContext>, ICustomerRepository
    {
        public CustomerRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await Context.Set<Customer>().FindAsync(id);
        }

        public async Task<Customer?> GetByIdentityNoAsync(string identityNo, CancellationToken cancellationToken = default)
        {
            return await Context.Customers
                .FirstOrDefaultAsync(c => c.IdentityNo == identityNo, cancellationToken);
        }

        public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await Context.Customers
                .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
        }

        public async Task<bool> IsIdentityNoExistsAsync(string identityNo, CancellationToken cancellationToken = default)
        {
            return await Context.Customers
                .AnyAsync(c => c.IdentityNo == identityNo, cancellationToken);
        }

        public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            return await Context.Customers
                .AnyAsync(c => c.Email == email, cancellationToken);
        }
    }
}

