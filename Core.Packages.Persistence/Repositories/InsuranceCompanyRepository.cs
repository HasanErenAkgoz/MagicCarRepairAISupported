using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class InsuranceCompanyRepository : EfEntityRepository<InsuranceCompany, BaseDbContext>, IInsuranceCompanyRepository
    {
        public InsuranceCompanyRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<InsuranceCompany?> GetByIdAsync(int id)
        {
            return await Context.Set<InsuranceCompany>()
                .Include(i => i.Client)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<InsuranceCompany?> GetByCompanyCodeAsync(string companyCode, CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsuranceCompany>()
                .Include(i => i.Client)
                .FirstOrDefaultAsync(i => i.CompanyCode == companyCode, cancellationToken);
        }

        public async Task<List<InsuranceCompany>> GetActiveCompaniesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<InsuranceCompany>()
                .Include(i => i.Client)
                .Where(i => i.IsActive)
                .OrderBy(i => i.CompanyName)
                .ToListAsync(cancellationToken);
        }
    }
}

