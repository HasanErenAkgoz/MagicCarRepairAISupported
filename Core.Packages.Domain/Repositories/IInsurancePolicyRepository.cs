using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IInsurancePolicyRepository : IEntityRepository<InsurancePolicy, int>
    {
        Task<InsurancePolicy?> GetByPolicyNumberAsync(string policyNumber, CancellationToken cancellationToken = default);
        Task<List<InsurancePolicy>> GetByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default);
        Task<List<InsurancePolicy>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
        Task<List<InsurancePolicy>> GetByInsuranceCompanyIdAsync(int insuranceCompanyId, CancellationToken cancellationToken = default);
        Task<List<InsurancePolicy>> GetActivePoliciesAsync(CancellationToken cancellationToken = default);
        Task<List<InsurancePolicy>> GetExpiringPoliciesAsync(int daysBeforeExpiration, CancellationToken cancellationToken = default);
        Task<InsurancePolicy?> GetActivePolicyForVehicleAsync(int vehicleId, CancellationToken cancellationToken = default);
    }
}

