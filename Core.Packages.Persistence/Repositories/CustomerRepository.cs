using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class CustomerRepository : EfEntityRepository<Customer, BaseDbContext>, ICustomerRepository
    {
        private readonly ITenantService _tenantService;

        public CustomerRepository(
            BaseDbContext context,
            IUnitOfWork unitOfWork,
            ITenantService tenantService) : base(context, unitOfWork)
        {
            _tenantService = tenantService;
        }

        public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await TenantCustomersQuery()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Customer?> GetByIdForTenantAsync(int id, int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Customers
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    c => c.Id == id && c.ClientId == clientId && c.Status != Status.Deleted,
                    cancellationToken);
        }

        public async Task<Customer?> GetByIdWithVehiclesAsync(int id, CancellationToken cancellationToken = default)
        {
            return await TenantCustomersQuery()
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        private IQueryable<Customer> TenantCustomersQuery()
        {
            var query = Context.Customers
                .IgnoreQueryFilters()
                .Where(c => c.Status != Status.Deleted);

            var clientId = _tenantService.GetCurrentClientId();
            if (clientId.HasValue)
                query = query.Where(c => c.ClientId == clientId.Value);

            return query;
        }

        public async Task<Customer?> GetByUserIdForTenantAsync(int userId, int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Customers
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    c => c.UserId == userId && c.ClientId == clientId && c.Status != Status.Deleted,
                    cancellationToken);
        }

        public async Task<Customer?> GetByIdentityNoAsync(string identityNo, CancellationToken cancellationToken = default)
        {
            return await TenantCustomersQuery()
                .FirstOrDefaultAsync(c => c.IdentityNo == identityNo, cancellationToken);
        }

        public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await TenantCustomersQuery()
                .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
        }

        public async Task<bool> IsIdentityNoExistsAsync(string identityNo, CancellationToken cancellationToken = default)
        {
            return await TenantCustomersQuery()
                .AnyAsync(c => c.IdentityNo == identityNo, cancellationToken);
        }

        public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            return await TenantCustomersQuery()
                .AnyAsync(c => c.Email == email, cancellationToken);
        }
    }
}
