using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class EmployeeRepository : EfEntityRepository<Employee, BaseDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await Context.Set<Employee>().FindAsync(id);
        }

        public async Task<Employee?> GetByEmployeeNoAsync(string employeeNo, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Employee>()
                .FirstOrDefaultAsync(e => e.EmployeeNo == employeeNo, cancellationToken);
        }

        public async Task<List<Employee>> GetByPositionAsync(EmployeePosition position, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Employee>()
                .Where(e => e.Position == position && e.EmploymentStatus == EmploymentStatus.Active)
                .OrderBy(e => e.FirstName)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Employee>> GetActiveEmployeesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<Employee>()
                .Where(e => e.EmploymentStatus == EmploymentStatus.Active)
                .OrderBy(e => e.Position)
                .ThenBy(e => e.FirstName)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsEmployeeNoExistsAsync(string employeeNo, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Employee>()
                .AnyAsync(e => e.EmployeeNo == employeeNo, cancellationToken);
        }
    }
}

