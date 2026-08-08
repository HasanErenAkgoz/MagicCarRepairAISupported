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

        public async Task DeleteAllByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
        {
            var employeeIds = await Context.Set<Employee>()
                .Where(e => e.ClientId == clientId)
                .Select(e => e.Id)
                .ToListAsync(cancellationToken);

            if (!employeeIds.Any()) return;

            // Null out nullable FKs
            await Context.Set<WorkOrder>()
                .Where(w => w.AssignedEmployeeId != null && employeeIds.Contains(w.AssignedEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(w => w.AssignedEmployeeId, (int?)null), cancellationToken);

            await Context.Set<Appointment>()
                .Where(a => a.AssignedEmployeeId != null && employeeIds.Contains(a.AssignedEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.AssignedEmployeeId, (int?)null), cancellationToken);

            await Context.Set<StockMovement>()
                .Where(s => s.EmployeeId != null && employeeIds.Contains(s.EmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.EmployeeId, (int?)null), cancellationToken);

            await Context.Set<Expense>()
                .Where(e => e.EmployeeId != null && employeeIds.Contains(e.EmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.EmployeeId, (int?)null), cancellationToken);

            await Context.Set<WorkOrderTimeline>()
                .Where(t => t.EmployeeId != null && employeeIds.Contains(t.EmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.EmployeeId, (int?)null), cancellationToken);

            await Context.Set<WorkOrderPhoto>()
                .Where(p => p.UploadedByEmployeeId != null && employeeIds.Contains(p.UploadedByEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.UploadedByEmployeeId, (int?)null), cancellationToken);

            await Context.Set<VehiclePhoto>()
                .Where(p => p.UploadedByEmployeeId != null && employeeIds.Contains(p.UploadedByEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.UploadedByEmployeeId, (int?)null), cancellationToken);

            await Context.Set<QuoteRequestPhoto>()
                .Where(p => p.UploadedByEmployeeId != null && employeeIds.Contains(p.UploadedByEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.UploadedByEmployeeId, (int?)null), cancellationToken);

            await Context.Set<QuoteResponse>()
                .Where(r => r.EmployeeId != null && employeeIds.Contains(r.EmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.EmployeeId, (int?)null), cancellationToken);

            await Context.Set<PartStock>()
                .Where(p => p.LastUpdatedByEmployeeId != null && employeeIds.Contains(p.LastUpdatedByEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.LastUpdatedByEmployeeId, (int?)null), cancellationToken);

            // Delete non-nullable FK child records
            await Context.Set<WorkOrderLabor>()
                .Where(l => employeeIds.Contains(l.EmployeeId))
                .ExecuteDeleteAsync(cancellationToken);

            await Context.Set<SalaryPayment>()
                .Where(s => employeeIds.Contains(s.EmployeeId))
                .ExecuteDeleteAsync(cancellationToken);

            // Delete employees
            await Context.Set<Employee>()
                .Where(e => e.ClientId == clientId)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var employeeIds = new List<int> { id };

            // Null out nullable FKs
            await Context.Set<WorkOrder>()
                .Where(w => w.AssignedEmployeeId != null && employeeIds.Contains(w.AssignedEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(w => w.AssignedEmployeeId, (int?)null), cancellationToken);

            await Context.Set<Appointment>()
                .Where(a => a.AssignedEmployeeId != null && employeeIds.Contains(a.AssignedEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.AssignedEmployeeId, (int?)null), cancellationToken);

            await Context.Set<StockMovement>()
                .Where(s => s.EmployeeId != null && employeeIds.Contains(s.EmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.EmployeeId, (int?)null), cancellationToken);

            await Context.Set<Expense>()
                .Where(e => e.EmployeeId != null && employeeIds.Contains(e.EmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.EmployeeId, (int?)null), cancellationToken);

            await Context.Set<WorkOrderTimeline>()
                .Where(t => t.EmployeeId != null && employeeIds.Contains(t.EmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.EmployeeId, (int?)null), cancellationToken);

            await Context.Set<WorkOrderPhoto>()
                .Where(p => p.UploadedByEmployeeId != null && employeeIds.Contains(p.UploadedByEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.UploadedByEmployeeId, (int?)null), cancellationToken);

            await Context.Set<VehiclePhoto>()
                .Where(p => p.UploadedByEmployeeId != null && employeeIds.Contains(p.UploadedByEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.UploadedByEmployeeId, (int?)null), cancellationToken);

            await Context.Set<QuoteRequestPhoto>()
                .Where(p => p.UploadedByEmployeeId != null && employeeIds.Contains(p.UploadedByEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.UploadedByEmployeeId, (int?)null), cancellationToken);

            await Context.Set<QuoteResponse>()
                .Where(r => r.EmployeeId != null && employeeIds.Contains(r.EmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.EmployeeId, (int?)null), cancellationToken);

            await Context.Set<PartStock>()
                .Where(p => p.LastUpdatedByEmployeeId != null && employeeIds.Contains(p.LastUpdatedByEmployeeId.Value))
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.LastUpdatedByEmployeeId, (int?)null), cancellationToken);

            // Delete non-nullable FK child records
            await Context.Set<WorkOrderLabor>()
                .Where(l => employeeIds.Contains(l.EmployeeId))
                .ExecuteDeleteAsync(cancellationToken);

            await Context.Set<SalaryPayment>()
                .Where(s => employeeIds.Contains(s.EmployeeId))
                .ExecuteDeleteAsync(cancellationToken);

            // Delete employee
            await Context.Set<Employee>()
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}

