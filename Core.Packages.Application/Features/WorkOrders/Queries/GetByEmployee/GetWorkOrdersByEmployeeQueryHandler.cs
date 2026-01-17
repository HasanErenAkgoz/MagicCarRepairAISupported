using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetByEmployee
{
    public class GetWorkOrdersByEmployeeQueryHandler : IRequestHandler<GetWorkOrdersByEmployeeQuery, GetWorkOrdersByEmployeeResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITenantService _tenantService;

        public GetWorkOrdersByEmployeeQueryHandler(
            IWorkOrderRepository workOrderRepository,
            IEmployeeRepository employeeRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _employeeRepository = employeeRepository;
            _tenantService = tenantService;
        }

        public async Task<GetWorkOrdersByEmployeeResponse> Handle(GetWorkOrdersByEmployeeQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Personel kontrolü
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.EmployeeId });
            }

            if (employee.ClientId != clientId)
            {
                throw new DomainException("EMPLOYEE_NOT_BELONG_TO_CLIENT", new { EmployeeId = request.EmployeeId });
            }

            // İş emirlerini getir
            var allWorkOrders = await _workOrderRepository.GetByEmployeeIdAsync(request.EmployeeId, cancellationToken);

            // Filtreleme
            var workOrders = allWorkOrders.AsQueryable();

            // Durum filtresi
            if (request.Status.HasValue)
            {
                workOrders = workOrders.Where(wo => wo.Status == request.Status.Value);
            }

            // Sadece aktif iş emirleri
            if (request.OnlyActive)
            {
                workOrders = workOrders.Where(wo => 
                    wo.Status != WorkOrderStatus.Delivered && 
                    wo.Status != WorkOrderStatus.Cancelled);
            }

            // Tarih filtresi
            if (request.StartDate.HasValue)
            {
                workOrders = workOrders.Where(wo => wo.EntryDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                workOrders = workOrders.Where(wo => wo.EntryDate <= request.EndDate.Value);
            }

            var filteredWorkOrders = workOrders.ToList();

            // İstatistikler hesapla
            var activeCount = filteredWorkOrders.Count(wo => 
                wo.Status != WorkOrderStatus.Delivered && 
                wo.Status != WorkOrderStatus.Cancelled);

            var completedCount = filteredWorkOrders.Count(wo => 
                wo.Status == WorkOrderStatus.Delivered);

            var totalRevenue = filteredWorkOrders
                .Where(wo => wo.Status == WorkOrderStatus.Delivered)
                .Sum(wo => wo.TotalAmount);

            // Response oluştur
            var response = new GetWorkOrdersByEmployeeResponse
            {
                Employee = new GetWorkOrdersByEmployeeResponse.EmployeeInfo
                {
                    Id = employee.Id,
                    EmployeeNo = employee.EmployeeNo,
                    FullName = employee.FullName,
                    Position = employee.Position.ToString(),
                    Email = employee.Email,
                    Phone = employee.Phone
                },
                WorkOrders = filteredWorkOrders.Select(wo => new GetWorkOrdersByEmployeeResponse.WorkOrderInfo
                {
                    Id = wo.Id,
                    WorkOrderNumber = wo.WorkOrderNumber,
                    CustomerId = wo.CustomerId,
                    CustomerName = wo.Customer?.FullName ?? "",
                    VehicleId = wo.VehicleId,
                    VehicleLicensePlate = wo.Vehicle?.LicensePlate ?? "",
                    VehicleBrand = wo.Vehicle?.Brand ?? "",
                    VehicleModel = wo.Vehicle?.Model ?? "",
                    EntryDate = wo.EntryDate,
                    EstimatedDeliveryDate = wo.EstimatedDeliveryDate,
                    ActualDeliveryDate = wo.ActualDeliveryDate,
                    Status = wo.Status,
                    StatusName = wo.Status.ToString(),
                    Priority = wo.Priority,
                    TotalAmount = wo.TotalAmount,
                    PaidAmount = wo.PaidAmount,
                    PaymentStatus = wo.PaymentStatus,
                    PaymentStatusName = wo.PaymentStatus.ToString(),
                    Notes = wo.Notes
                }).ToList(),
                TotalCount = filteredWorkOrders.Count,
                ActiveCount = activeCount,
                CompletedCount = completedCount,
                TotalRevenue = totalRevenue
            };

            return response;
        }
    }
}
