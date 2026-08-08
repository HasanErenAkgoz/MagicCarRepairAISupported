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

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateStatus
{
    public class UpdateWorkOrderStatusCommandHandler : IRequestHandler<UpdateWorkOrderStatusCommand, UpdateWorkOrderStatusResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<Employee, int> _employeeRepository;
        private readonly ISignalRNotificationService _signalRNotificationService;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public UpdateWorkOrderStatusCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<Employee, int> employeeRepository,
            ISignalRNotificationService signalRNotificationService,
            IMapper mapper,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _employeeRepository = employeeRepository;
            _signalRNotificationService = signalRNotificationService;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<UpdateWorkOrderStatusResponse> Handle(UpdateWorkOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // WorkOrder'ı bul
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            // Client kontrolü
            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            var oldStatus = workOrder.Status;

            // Durum değişikliği validasyonu
            if (oldStatus == request.NewStatus)
            {
                throw new DomainException("WORKORDER_STATUS_ALREADY_SET", new { Status = request.NewStatus });
            }

            // Employee kontrolü (opsiyonel)
            if (request.EmployeeId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);
                if (employee == null || employee.ClientId != clientId)
                {
                    throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.EmployeeId.Value });
                }
            }

            // Durum değiştir
            workOrder.ChangeStatus(request.NewStatus, request.EmployeeId, request.Description);

            // WorkOrder'ı güncelle
            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Timeline kaydı oluştur
            var timeline = new WorkOrderTimeline
            {
                WorkOrderId = workOrder.Id,
                EventDate = DateTime.UtcNow,
                StatusChange = request.NewStatus,
                OldStatus = oldStatus,
                NewStatus = request.NewStatus,
                EmployeeId = request.EmployeeId,
                Description = request.Description ?? $"Durum değiştirildi: {oldStatus} -> {request.NewStatus}",
                EventType = "StatusChange"
            };

            await _workOrderRepository.AddTimelineAsync(timeline, cancellationToken);

            // Real-time bildirim gönder
            await _signalRNotificationService.SendWorkOrderUpdateAsync(
                workOrder.Id,
                request.NewStatus.ToString(),
                timeline.Description,
                workOrder.CustomerId);

            // Response
            return new UpdateWorkOrderStatusResponse
            {
                WorkOrderId = workOrder.Id,
                OldStatus = oldStatus,
                NewStatus = request.NewStatus,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}

