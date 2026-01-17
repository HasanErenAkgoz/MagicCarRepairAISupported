using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class InsurancePolicyRepository : EfEntityRepository<InsurancePolicy, BaseDbContext>, IInsurancePolicyRepository
    {
        public InsurancePolicyRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<InsurancePolicy?> GetByIdAsync(int id)
        {
            return await Context.Set<InsurancePolicy>()
                .Include(p => p.Client)
                .Include(p => p.Vehicle)
                .Include(p => p.Customer)
                .Include(p => p.InsuranceCompany)
                .Include(p => p.PolicyFile)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<InsurancePolicy?> GetByPolicyNumberAsync(string policyNumber, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsurancePolicy>()
                .Include(p => p.Vehicle)
                .Include(p => p.Customer)
                .Include(p => p.InsuranceCompany)
                .FirstOrDefaultAsync(p => p.PolicyNumber == policyNumber, cancellationToken);
        }

        public async Task<List<InsurancePolicy>> GetByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsurancePolicy>()
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Customer)
                .Where(p => p.VehicleId == vehicleId)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<InsurancePolicy>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsurancePolicy>()
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Vehicle)
                .Where(p => p.CustomerId == customerId)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<InsurancePolicy>> GetByInsuranceCompanyIdAsync(int insuranceCompanyId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsurancePolicy>()
                .Include(p => p.Vehicle)
                .Include(p => p.Customer)
                .Where(p => p.InsuranceCompanyId == insuranceCompanyId)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<InsurancePolicy>> GetActivePoliciesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            return await Context.Set<InsurancePolicy>()
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Vehicle)
                .Include(p => p.Customer)
                .Where(p => p.Status == InsuranceStatus.Active && 
                           p.StartDate <= now && 
                           p.EndDate >= now)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<InsurancePolicy>> GetExpiringPoliciesAsync(int daysBeforeExpiration, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var expirationThreshold = now.AddDays(daysBeforeExpiration);

            return await Context.Set<InsurancePolicy>()
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Vehicle)
                .Include(p => p.Customer)
                .Where(p => p.Status == InsuranceStatus.Active &&
                           p.EndDate >= now &&
                           p.EndDate <= expirationThreshold)
                .OrderBy(p => p.EndDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<InsurancePolicy?> GetActivePolicyForVehicleAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            return await Context.Set<InsurancePolicy>()
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Customer)
                .Where(p => p.VehicleId == vehicleId &&
                           p.Status == InsuranceStatus.Active &&
                           p.StartDate <= now &&
                           p.EndDate >= now)
                .OrderByDescending(p => p.StartDate)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

