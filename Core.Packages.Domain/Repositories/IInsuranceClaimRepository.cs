using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IInsuranceClaimRepository : IEntityRepository<InsuranceClaim, int>
    {
        Task<InsuranceClaim?> GetByClaimNumberAsync(string claimNumber, CancellationToken cancellationToken = default);
        Task<List<InsuranceClaim>> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default);
        Task<List<InsuranceClaim>> GetByInsurancePolicyIdAsync(int insurancePolicyId, CancellationToken cancellationToken = default);
        Task<List<InsuranceClaim>> GetByStatusAsync(ClaimStatus status, CancellationToken cancellationToken = default);
        Task<InsuranceClaim?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default);
        Task<List<InsuranceClaim>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
    }
}

