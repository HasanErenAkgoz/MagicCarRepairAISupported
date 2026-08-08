using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class WorkOrderRepository : EfEntityRepository<WorkOrder, BaseDbContext>, IWorkOrderRepository
    {
        public WorkOrderRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<WorkOrder?> GetByIdAsync(int id)
        {
            return await Context.Set<WorkOrder>()
                .Include(w => w.Vehicle)
                .Include(w => w.Customer)
                .Include(w => w.AssignedEmployee)
                .Include(w => w.Items)
                    .ThenInclude(i => i.Part)
                .Include(w => w.Labors)
                    .ThenInclude(l => l.Employee)
                .Include(w => w.Timeline)
                    .ThenInclude(t => t.Employee)
                .Include(w => w.Photos)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WorkOrder?> GetByWorkOrderNumberAsync(string workOrderNumber, CancellationToken cancellationToken = default)
        {
            return await Context.Set<WorkOrder>()
                .FirstOrDefaultAsync(w => w.WorkOrderNumber == workOrderNumber, cancellationToken);
        }

        public async Task<List<WorkOrder>> GetByStatusAsync(WorkOrderStatus status, CancellationToken cancellationToken = default)
        {
            return await Context.Set<WorkOrder>()
                .Include(w => w.Vehicle)
                .Include(w => w.Customer)
                .Include(w => w.AssignedEmployee)
                .Where(w => w.Status == status)
                .OrderByDescending(w => w.EntryDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<WorkOrder>> GetActiveWorkOrdersAsync(CancellationToken cancellationToken = default)
        {
            // List<T> avoids array.Contains → ReadOnlySpan binding (EF Core + .NET 9+ TypeLoadException).
            var activeStatuses = new List<WorkOrderStatus>
            {
                WorkOrderStatus.AppointmentScheduled,
                WorkOrderStatus.VehicleEntered,
                WorkOrderStatus.DiagnosisCompleted,
                WorkOrderStatus.WaitingForParts,
                WorkOrderStatus.InProgress,
                WorkOrderStatus.InRepair,
                WorkOrderStatus.QualityControl,
                WorkOrderStatus.Washing,
                WorkOrderStatus.ReadyForDelivery
            };

            return await Context.Set<WorkOrder>()
                .Include(w => w.Vehicle)
                .Include(w => w.Customer)
                .Include(w => w.AssignedEmployee)
                .Where(w => activeStatuses.Contains(w.Status))
                .OrderByDescending(w => w.EntryDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<WorkOrder>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<WorkOrder>()
                .Include(w => w.Vehicle)
                .Include(w => w.Customer)
                .Include(w => w.AssignedEmployee)
                .OrderByDescending(w => w.EntryDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<WorkOrder>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<WorkOrder>()
                .Include(w => w.Vehicle)
                .Include(w => w.Customer)
                .Where(w => w.AssignedEmployeeId == employeeId)
                .OrderByDescending(w => w.EntryDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<WorkOrder>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<WorkOrder>()
                .IgnoreQueryFilters()
                .Include(w => w.Vehicle)
                .Include(w => w.Customer)
                .Include(w => w.AssignedEmployee)
                .Where(w => w.CustomerId == customerId)
                .OrderByDescending(w => w.EntryDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<WorkOrder>> GetByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<WorkOrder>()
                .Include(w => w.Customer)
                .Include(w => w.Vehicle)
                .Include(w => w.AssignedEmployee)
                .Where(w => w.VehicleId == vehicleId)
                .OrderByDescending(w => w.EntryDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<WorkOrder?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<WorkOrder>()
                .Include(w => w.Vehicle)
                    .ThenInclude(v => v.Customer)
                .Include(w => w.Vehicle)
                    .ThenInclude(v => v.Photos)
                        .ThenInclude(p => p.UploadedFile)
                .Include(w => w.Customer)
                .Include(w => w.AssignedEmployee)
                .Include(w => w.Items)
                    .ThenInclude(i => i.Part)
                .Include(w => w.Labors)
                    .ThenInclude(l => l.Employee)
                .Include(w => w.Timeline)
                    .ThenInclude(t => t.Employee)
                .Include(w => w.Photos)
                    .ThenInclude(p => p.UploadedFile)
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public async Task<WorkOrderTimeline> AddTimelineAsync(WorkOrderTimeline timeline, CancellationToken cancellationToken = default)
        {
            await Context.Set<WorkOrderTimeline>().AddAsync(timeline, cancellationToken);
            await SaveChangesAsync();
            return timeline;
        }
    }
}
