using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetAll
{
    public class GetAllWorkOrdersQueryHandler : IRequestHandler<GetAllWorkOrdersQuery, List<GetAllWorkOrdersResponse>>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetAllWorkOrdersQueryHandler(
            IWorkOrderRepository workOrderRepository,
            IMapper mapper,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<List<GetAllWorkOrdersResponse>> Handle(GetAllWorkOrdersQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            List<Domain.Entities.WorkOrder> workOrders;

            if (request.Status.HasValue)
            {
                workOrders = await _workOrderRepository.GetByStatusAsync(request.Status.Value, cancellationToken);
            }
            else if (request.CustomerId.HasValue)
            {
                workOrders = await _workOrderRepository.GetByCustomerIdAsync(request.CustomerId.Value, cancellationToken);
            }
            else if (request.VehicleId.HasValue)
            {
                workOrders = await _workOrderRepository.GetByVehicleIdAsync(request.VehicleId.Value, cancellationToken);
            }
            else if (request.EmployeeId.HasValue)
            {
                workOrders = await _workOrderRepository.GetByEmployeeIdAsync(request.EmployeeId.Value, cancellationToken);
            }
            else
            {
                // Tüm work order'ları getir (pagination eklenebilir)
                var allWorkOrders = await _workOrderRepository.GetListAsync(cancellationToken);
                workOrders = allWorkOrders.ToList();
            }

            // Tarih filtresi
            if (request.StartDate.HasValue || request.EndDate.HasValue)
            {
                workOrders = workOrders.Where(wo =>
                    (!request.StartDate.HasValue || wo.EntryDate >= request.StartDate.Value) &&
                    (!request.EndDate.HasValue || wo.EntryDate <= request.EndDate.Value)
                ).ToList();
            }

            var responses = workOrders.Select(wo => new GetAllWorkOrdersResponse
            {
                Id = wo.Id,
                WorkOrderNumber = wo.WorkOrderNumber,
                VehicleId = wo.VehicleId,
                VehicleLicensePlate = wo.Vehicle?.LicensePlate ?? "",
                VehicleBrand = wo.Vehicle?.Brand ?? "",
                VehicleModel = wo.Vehicle?.Model ?? "",
                CustomerId = wo.CustomerId,
                CustomerName = wo.Customer?.FullName ?? "",
                EntryDate = wo.EntryDate,
                EstimatedDeliveryDate = wo.EstimatedDeliveryDate,
                Status = wo.Status,
                StatusName = wo.Status.ToString(),
                Priority = wo.Priority,
                TotalAmount = wo.TotalAmount,
                PaymentStatus = wo.PaymentStatus,
                PaymentStatusName = wo.PaymentStatus.ToString(),
                AssignedEmployeeId = wo.AssignedEmployeeId,
                AssignedEmployeeName = wo.AssignedEmployee?.FullName
            }).ToList();

            return responses;
        }
    }
}

