using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddLabor
{
    public class AddWorkOrderLaborCommandHandler : IRequestHandler<AddWorkOrderLaborCommand, AddWorkOrderLaborResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<Employee, int> _employeeRepository;
        private readonly IEntityRepository<WorkOrderLabor, int> _workOrderLaborRepository;
        private readonly ITenantService _tenantService;

        public AddWorkOrderLaborCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<Employee, int> employeeRepository,
            IEntityRepository<WorkOrderLabor, int> workOrderLaborRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _employeeRepository = employeeRepository;
            _workOrderLaborRepository = workOrderLaborRepository;
            _tenantService = tenantService;
        }

        public async Task<AddWorkOrderLaborResponse> Handle(AddWorkOrderLaborCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // İş emri tamamlanmış veya teslim edilmişse labor eklenemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_ADD_LABOR", new { Status = workOrder.Status });
            }

            // Employee kontrolü
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null || employee.ClientId != clientId)
            {
                throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.EmployeeId });
            }

            // WorkOrderLabor oluştur
            var labor = new WorkOrderLabor
            {
                WorkOrderId = request.WorkOrderId,
                EmployeeId = request.EmployeeId,
                OperationName = request.OperationName,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                DurationHours = request.DurationHours,
                HourlyRate = request.HourlyRate,
                Description = request.Description
            };

            // Süre ve toplam tutarı hesapla
            labor.CalculateDuration();
            labor.CalculateTotal();

            // Labor'ı ekle
            await _workOrderLaborRepository.AddAsync(labor, cancellationToken);

            // WorkOrder toplamını güncelle
            workOrder.CalculateTotal();
            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            return new AddWorkOrderLaborResponse
            {
                LaborId = labor.Id,
                WorkOrderId = workOrder.Id,
                TotalAmount = labor.TotalAmount,
                WorkOrderTotalAmount = workOrder.TotalAmount
            };
        }
    }
}

