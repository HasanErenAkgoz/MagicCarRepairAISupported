using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetFleetStatus
{
    public class GetFleetStatusQueryHandler : IRequestHandler<GetFleetStatusQuery, GetFleetStatusResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetFleetStatusQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<GetFleetStatusResponse> Handle(GetFleetStatusQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var workOrders = _workOrderRepository.Query()
                .AsNoTracking()
                .Where(wo => wo.ClientId == clientId);

            // Repairing: InProgress, InRepair, DiagnosisCompleted, QualityControl, Washing
            var repairing = await workOrders
                .Where(wo => wo.Status == WorkOrderStatus.InProgress ||
                           wo.Status == WorkOrderStatus.InRepair ||
                           wo.Status == WorkOrderStatus.DiagnosisCompleted ||
                           wo.Status == WorkOrderStatus.QualityControl ||
                           wo.Status == WorkOrderStatus.Washing)
                .CountAsync(cancellationToken);

            // Completed: Delivered
            var completed = await workOrders
                .Where(wo => wo.Status == WorkOrderStatus.Delivered)
                .CountAsync(cancellationToken);

            // Waiting: VehicleEntered, AppointmentScheduled, WaitingForParts, ReadyForDelivery
            var waiting = await workOrders
                .Where(wo => wo.Status == WorkOrderStatus.VehicleEntered ||
                           wo.Status == WorkOrderStatus.AppointmentScheduled ||
                           wo.Status == WorkOrderStatus.WaitingForParts ||
                           wo.Status == WorkOrderStatus.ReadyForDelivery)
                .CountAsync(cancellationToken);

            // Total (excluding Cancelled)
            var total = await workOrders
                .Where(wo => wo.Status != WorkOrderStatus.Cancelled)
                .CountAsync(cancellationToken);

            // Efficiency: (Completed / Total) * 100
            decimal efficiency = 0;
            if (total > 0)
            {
                efficiency = (decimal)completed / total * 100;
            }

            return new GetFleetStatusResponse
            {
                Repairing = repairing,
                Completed = completed,
                Waiting = waiting,
                Efficiency = efficiency
            };
        }
    }
}
