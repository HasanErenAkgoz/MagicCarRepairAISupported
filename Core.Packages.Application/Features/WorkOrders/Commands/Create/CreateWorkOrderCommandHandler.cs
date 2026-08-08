using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Create
{
    public class CreateWorkOrderCommandHandler : IRequestHandler<CreateWorkOrderCommand, CreateWorkOrderResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<Vehicle, int> _vehicleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEntityRepository<Employee, int> _employeeRepository;
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;
        private readonly ILogger<CreateWorkOrderCommandHandler> _logger;
        private readonly ISignalRNotificationService _signalRNotificationService;

        public CreateWorkOrderCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<Vehicle, int> vehicleRepository,
            ICustomerRepository customerRepository,
            IEntityRepository<Employee, int> employeeRepository,
            IInsurancePolicyRepository insurancePolicyRepository,
            IMapper mapper,
            ITenantService tenantService,
            ILogger<CreateWorkOrderCommandHandler> logger,
            ISignalRNotificationService signalRNotificationService)
        {
            _workOrderRepository = workOrderRepository;
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _insurancePolicyRepository = insurancePolicyRepository;
            _mapper = mapper;
            _tenantService = tenantService;
            _logger = logger;
            _signalRNotificationService = signalRNotificationService;
        }

        public async Task<CreateWorkOrderResponse> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // Vehicle kontrolü
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null || vehicle.ClientId != clientId)
            {
                throw new DomainException("VEHICLE_NOT_FOUND", new { VehicleId = request.VehicleId });
            }

            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer == null)
            {
                throw new DomainException("CUSTOMER_NOT_FOUND", new { CustomerId = request.CustomerId });
            }

            // Vehicle'ın Customer'a ait olduğunu kontrol et
            if (vehicle.CustomerId != request.CustomerId)
            {
                throw new DomainException("VEHICLE_NOT_BELONG_TO_CUSTOMER", new { VehicleId = request.VehicleId, CustomerId = request.CustomerId });
            }

            // AssignedEmployee kontrolü (opsiyonel)
            if (request.AssignedEmployeeId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.AssignedEmployeeId.Value);
                if (employee == null || employee.ClientId != clientId)
                {
                    throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.AssignedEmployeeId.Value });
                }
            }

            // Check for active insurance policy for the vehicle
            var activePolicy = await _insurancePolicyRepository.GetActivePolicyForVehicleAsync(request.VehicleId, cancellationToken);
            if (activePolicy != null)
            {
                // Log information about active insurance policy
                _logger.LogInformation(
                    "Active insurance policy found for Vehicle {VehicleId}: PolicyNumber={PolicyNumber}, InsuranceCompany={CompanyName}, ValidUntil={EndDate}",
                    request.VehicleId,
                    activePolicy.PolicyNumber,
                    activePolicy.InsuranceCompany?.CompanyName ?? "N/A",
                    activePolicy.EndDate);
                
                // Note: Insurance claim can be created separately via CreateInsuranceClaim endpoint
            }

            // WorkOrder oluştur
            var workOrder = new WorkOrder
            {
                WorkOrderNumber = WorkOrder.GenerateWorkOrderNumber(),
                VehicleId = request.VehicleId,
                CustomerId = request.CustomerId,
                EntryDate = request.EntryDate ?? DateTime.UtcNow,
                EstimatedDeliveryDate = request.EstimatedDeliveryDate,
                Priority = request.Priority,
                Kilometers = request.Kilometers,
                FuelLevel = request.FuelLevel,
                CustomerComplaints = request.CustomerComplaints,
                SpecialRequests = request.SpecialRequests,
                AssignedEmployeeId = request.AssignedEmployeeId,
                Notes = request.Notes,
                Status = Domain.Enums.WorkOrderStatus.VehicleEntered // Araç girişi yapıldı
            };

            // WorkOrder kaydet
            await _workOrderRepository.AddAsync(workOrder, cancellationToken);

            // Timeline kaydı oluştur
            var timeline = new WorkOrderTimeline
            {
                WorkOrderId = workOrder.Id,
                EventDate = DateTime.UtcNow,
                StatusChange = Domain.Enums.WorkOrderStatus.VehicleEntered,
                OldStatus = Domain.Enums.WorkOrderStatus.AppointmentScheduled,
                NewStatus = Domain.Enums.WorkOrderStatus.VehicleEntered,
                Description = "İş emri oluşturuldu ve araç girişi yapıldı.",
                EventType = "StatusChange"
            };

            await _workOrderRepository.AddTimelineAsync(timeline, cancellationToken);

            // Real-time bildirim gönder
            await _signalRNotificationService.SendWorkOrderUpdateAsync(
                workOrder.Id,
                workOrder.Status.ToString(),
                $"Yeni iş emri oluşturuldu: {workOrder.WorkOrderNumber}",
                workOrder.CustomerId);

            // Dashboard güncellemesi için client'a bildirim
            await _signalRNotificationService.SendNotificationToClientAsync(
                clientId,
                "Yeni İş Emri",
                $"İş emri #{workOrder.WorkOrderNumber} oluşturuldu",
                "WorkOrder",
                workOrder.Id);

            // Response
            var response = _mapper.Map<CreateWorkOrderResponse>(workOrder);
            return response;
        }
    }
}

