using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Update
{
    public class UpdateWorkOrderCommandHandler : IRequestHandler<UpdateWorkOrderCommand, UpdateWorkOrderResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;
        private readonly ISignalRNotificationService _signalRNotificationService;

        public UpdateWorkOrderCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEmployeeRepository employeeRepository,
            ITenantService tenantService,
            IMapper mapper,
            ISignalRNotificationService signalRNotificationService)
        {
            _workOrderRepository = workOrderRepository;
            _employeeRepository = employeeRepository;
            _tenantService = tenantService;
            _mapper = mapper;
            _signalRNotificationService = signalRNotificationService;
        }

        public async Task<UpdateWorkOrderResponse> Handle(UpdateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder'ı bul
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // İş emri teslim edilmiş veya iptal edilmişse güncellenemez
            if (workOrder.Status == Domain.Enums.WorkOrderStatus.Delivered || 
                workOrder.Status == Domain.Enums.WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_BE_UPDATED", new { Status = workOrder.Status });
            }

            // Employee kontrolü (eğer atanacaksa)
            if (request.AssignedEmployeeId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.AssignedEmployeeId.Value);
                if (employee == null || employee.ClientId != clientId)
                {
                    throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.AssignedEmployeeId.Value });
                }
            }

            // Güncellemeleri yap
            if (request.EstimatedDeliveryDate.HasValue)
            {
                workOrder.EstimatedDeliveryDate = request.EstimatedDeliveryDate.Value;
            }

            if (request.Priority.HasValue)
            {
                workOrder.Priority = request.Priority.Value;
            }

            if (request.Kilometers.HasValue)
            {
                workOrder.Kilometers = request.Kilometers.Value;
            }

            if (request.FuelLevel.HasValue)
            {
                if (request.FuelLevel.Value < 0 || request.FuelLevel.Value > 100)
                {
                    throw new DomainException("INVALID_FUEL_LEVEL", new { FuelLevel = request.FuelLevel.Value });
                }
                workOrder.FuelLevel = request.FuelLevel.Value;
            }

            if (request.CustomerComplaints != null)
            {
                workOrder.CustomerComplaints = request.CustomerComplaints;
            }

            if (request.SpecialRequests != null)
            {
                workOrder.SpecialRequests = request.SpecialRequests;
            }

            if (request.AssignedEmployeeId.HasValue)
            {
                workOrder.AssignedEmployeeId = request.AssignedEmployeeId.Value;
            }

            if (request.Notes != null)
            {
                workOrder.Notes = request.Notes;
            }

            // İndirim güncellemeleri
            if (request.DiscountPercentage.HasValue)
            {
                workOrder.DiscountPercentage = request.DiscountPercentage.Value;
            }

            if (request.DiscountAmount.HasValue)
            {
                workOrder.DiscountAmount = request.DiscountAmount.Value;
            }

            // Toplam tutarı yeniden hesapla
            workOrder.CalculateTotal();

            // Kaydet
            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Önemli güncellemeler için real-time bildirim gönder
            bool shouldNotify = false;
            string notificationMessage = string.Empty;

            if (request.AssignedEmployeeId.HasValue && workOrder.AssignedEmployeeId != request.AssignedEmployeeId.Value)
            {
                shouldNotify = true;
                notificationMessage = $"İş emri #{workOrder.WorkOrderNumber} için personel atandı: {workOrder.AssignedEmployee?.FullName}";
            }
            else if (request.EstimatedDeliveryDate.HasValue)
            {
                shouldNotify = true;
                notificationMessage = $"İş emri #{workOrder.WorkOrderNumber} için tahmini teslimat tarihi güncellendi: {request.EstimatedDeliveryDate.Value:dd.MM.yyyy}";
            }
            else if (request.Priority.HasValue)
            {
                shouldNotify = true;
                notificationMessage = $"İş emri #{workOrder.WorkOrderNumber} önceliği güncellendi: {request.Priority.Value}";
            }

            if (shouldNotify)
            {
                await _signalRNotificationService.SendWorkOrderUpdateAsync(
                    workOrder.Id,
                    workOrder.Status.ToString(),
                    notificationMessage,
                    workOrder.CustomerId);
            }

            // Response
            var response = _mapper.Map<UpdateWorkOrderResponse>(workOrder);
            response.AssignedEmployeeName = workOrder.AssignedEmployee?.FullName;
            response.UpdatedDate = DateTime.UtcNow;

            return response;
        }
    }
}
