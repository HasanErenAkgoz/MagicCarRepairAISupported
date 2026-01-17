using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetActive
{
    public class GetActiveWorkOrdersQueryHandler : IRequestHandler<GetActiveWorkOrdersQuery, List<GetActiveWorkOrdersResponse>>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetActiveWorkOrdersQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<List<GetActiveWorkOrdersResponse>> Handle(GetActiveWorkOrdersQuery request, CancellationToken cancellationToken)
        {
            var workOrders = await _workOrderRepository.GetActiveWorkOrdersAsync(cancellationToken);

            return workOrders.Select(wo => new GetActiveWorkOrdersResponse
            {
                Id = wo.Id,
                WorkOrderNumber = wo.WorkOrderNumber,
                VehicleLicensePlate = wo.Vehicle?.LicensePlate ?? "",
                CustomerName = wo.Customer?.FullName ?? "",
                EntryDate = wo.EntryDate,
                EstimatedDeliveryDate = wo.EstimatedDeliveryDate,
                Status = wo.Status,
                StatusName = wo.Status.ToString(),
                Priority = wo.Priority,
                TotalAmount = wo.TotalAmount,
                AssignedEmployeeId = wo.AssignedEmployeeId,
                AssignedEmployeeName = wo.AssignedEmployee?.FullName
            }).ToList();
        }
    }
}

