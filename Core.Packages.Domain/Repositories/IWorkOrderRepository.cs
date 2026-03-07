using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IWorkOrderRepository : IEntityRepository<WorkOrder, int>
    {
        Task<WorkOrder?> GetByWorkOrderNumberAsync(string workOrderNumber, CancellationToken cancellationToken = default);
        Task<List<WorkOrder>> GetByStatusAsync(WorkOrderStatus status, CancellationToken cancellationToken = default);
        Task<List<WorkOrder>> GetActiveWorkOrdersAsync(CancellationToken cancellationToken = default);
        Task<List<WorkOrder>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<List<WorkOrder>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
        Task<List<WorkOrder>> GetByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default);
        Task<List<WorkOrder>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
        Task<WorkOrder?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default);
        Task<WorkOrderTimeline> AddTimelineAsync(WorkOrderTimeline timeline, CancellationToken cancellationToken = default);
    }
}
