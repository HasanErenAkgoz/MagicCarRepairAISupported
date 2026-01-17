using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IInsuranceCompanyRepository : IEntityRepository<InsuranceCompany, int>
    {
        Task<InsuranceCompany?> GetByCompanyCodeAsync(string companyCode, CancellationToken cancellationToken = default);
        Task<List<InsuranceCompany>> GetActiveCompaniesAsync(CancellationToken cancellationToken = default);
    }
}

