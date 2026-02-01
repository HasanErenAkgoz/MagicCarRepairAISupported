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
                // Tüm work order'ları getir (filtre yoksa)
                workOrders = await _workOrderRepository.GetAllWithDetailsAsync(cancellationToken);
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
                Vehicle = wo.Vehicle != null ? new VehicleDto
                {
                    Id = wo.Vehicle.Id,
                    LicensePlate = wo.Vehicle.LicensePlate,
                    Brand = wo.Vehicle.Brand,
                    Model = wo.Vehicle.Model,
                    Year = wo.Vehicle.Year
                } : null,
                CustomerId = wo.CustomerId,
                Customer = wo.Customer != null ? new CustomerDto
                {
                    Id = wo.Customer.Id,
                    FirstName = wo.Customer.FirstName,
                    LastName = wo.Customer.LastName,
                    FullName = wo.Customer.FullName,
                    Phone = wo.Customer.PhoneNumber,
                    Avatar = wo.Customer.Avatar
                } : null,
                EntryDate = wo.EntryDate,
                EstimatedDeliveryDate = wo.EstimatedDeliveryDate,
                Status = wo.Status,
                StatusName = wo.Status.ToString(),
                Priority = wo.Priority,
                TotalAmount = wo.TotalAmount,
                EstimatedCost = wo.EstimatedCost,
                PaymentStatus = wo.PaymentStatus,
                PaymentStatusName = wo.PaymentStatus.ToString(),
                AssignedEmployeeId = wo.AssignedEmployeeId,
                AssignedEmployeeName = wo.AssignedEmployee?.FullName
            }).ToList();

            return responses;
        }
    }
}

