using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class InsuranceClaimRepository : EfEntityRepository<InsuranceClaim, BaseDbContext>, IInsuranceClaimRepository
    {
        public InsuranceClaimRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<InsuranceClaim?> GetByIdAsync(int id)
        {
            return await Context.Set<InsuranceClaim>()
                .Include(c => c.Client)
                .Include(c => c.WorkOrder)
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.InsuranceCompany)
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.Vehicle)
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.Customer)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<InsuranceClaim?> GetByClaimNumberAsync(string claimNumber, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsuranceClaim>()
                .Include(c => c.WorkOrder)
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.InsuranceCompany)
                .FirstOrDefaultAsync(c => c.ClaimNumber == claimNumber, cancellationToken);
        }

        public async Task<List<InsuranceClaim>> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsuranceClaim>()
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.InsuranceCompany)
                .Where(c => c.WorkOrderId == workOrderId)
                .OrderByDescending(c => c.DamageDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<InsuranceClaim>> GetByInsurancePolicyIdAsync(int insurancePolicyId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsuranceClaim>()
                .Include(c => c.WorkOrder)
                .Where(c => c.InsurancePolicyId == insurancePolicyId)
                .OrderByDescending(c => c.DamageDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<InsuranceClaim>> GetByStatusAsync(ClaimStatus status, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsuranceClaim>()
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.InsuranceCompany)
                .Include(c => c.WorkOrder)
                .Where(c => c.Status == status)
                .OrderByDescending(c => c.DamageDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<InsuranceClaim?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsuranceClaim>()
                .Include(c => c.Client)
                .Include(c => c.WorkOrder)
                    .ThenInclude(wo => wo!.Vehicle)
                .Include(c => c.WorkOrder)
                    .ThenInclude(wo => wo!.Customer)
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.InsuranceCompany)
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.Vehicle)
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p.Customer)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}

