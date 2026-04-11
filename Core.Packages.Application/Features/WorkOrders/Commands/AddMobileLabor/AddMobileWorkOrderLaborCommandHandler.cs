using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobileLabor
{
    public class AddMobileWorkOrderLaborCommandHandler : IRequestHandler<AddMobileWorkOrderLaborCommand, AddMobileWorkOrderLaborResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderLabor, int> _workOrderLaborRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddMobileWorkOrderLaborCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<WorkOrderLabor, int> workOrderLaborRepository,
            IEmployeeRepository employeeRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderLaborRepository = workOrderLaborRepository;
            _employeeRepository = employeeRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddMobileWorkOrderLaborResponse> Handle(AddMobileWorkOrderLaborCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.WorkOrderId, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.WorkOrderId });
            }

            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder'ı bul
            var workOrder = await _workOrderRepository.Query()
                .Include(wo => wo.Items)
                .Include(wo => wo.Labors)
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId && wo.ClientId == clientId, cancellationToken);

            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { Id = request.WorkOrderId });
            }

            // İş emri teslim edilmiş veya iptal edilmişse işçilik eklenemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_ADD_LABOR", new { Status = workOrder.Status.ToString() });
            }

            // Validation
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                throw new DomainException("LABOR_DESCRIPTION_REQUIRED", new { Message = "İşçilik açıklaması boş olamaz." });
            }

            if (request.Hours <= 0)
            {
                throw new DomainException("INVALID_HOURS", new { Message = "Saat 0'dan büyük olmalıdır." });
            }

            if (request.HourlyRate < 0)
            {
                throw new DomainException("INVALID_HOURLY_RATE", new { Message = "Saat ücreti negatif olamaz." });
            }

            // EmployeeId - WorkOrder'daki assigned employee veya current user'dan employee bul
            var employeeId = workOrder.AssignedEmployeeId;
            if (!employeeId.HasValue)
            {
                // Current user'dan employee bul
                var userId = GetCurrentUserId();
                if (userId > 0)
                {
                    var user = await _employeeRepository.Query()
                        .FirstOrDefaultAsync(e => e.UserId == userId && e.ClientId == clientId, cancellationToken);
                    if (user != null)
                    {
                        employeeId = user.Id;
                    }
                }

                if (!employeeId.HasValue)
                {
                    throw new DomainException("EMPLOYEE_REQUIRED_FOR_LABOR", new { Message = "İşçilik eklemek için teknisyen atanmalıdır." });
                }
            }

            // Employee kontrolü
            var employee = await _employeeRepository.GetByIdAsync(employeeId.Value);
            if (employee == null || employee.ClientId != clientId)
            {
                throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = employeeId.Value });
            }

            // WorkOrderLabor oluştur
            var labor = new WorkOrderLabor
            {
                WorkOrderId = workOrder.Id,
                EmployeeId = employeeId.Value,
                OperationName = request.Description,
                DurationHours = request.Hours,
                HourlyRate = request.HourlyRate,
                ClientId = clientId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = GetCurrentUserId(),
                Status = Domain.Enums.Status.Active
            };

            // TotalAmount hesapla
            labor.CalculateTotal();

            // Labor'ı ekle
            await _workOrderLaborRepository.AddAsync(labor, cancellationToken);

            // WorkOrder toplamını güncelle
            workOrder.CalculateTotal();
            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Güncellenmiş maliyet özetini hesapla
            var partsSubtotal = workOrder.Items
                .Where(i => i.ItemType == WorkOrderItemType.Part)
                .Sum(i => i.TotalAmount);
            var laborSubtotal = workOrder.Labors.Sum(l => l.TotalAmount);
            var subtotal = partsSubtotal + laborSubtotal;
            var taxRate = workOrder.TaxAmount > 0 && workOrder.SubTotal > 0
                ? (workOrder.TaxAmount / workOrder.SubTotal) * 100
                : 20;
            var taxAmount = subtotal * (taxRate / 100);
            var total = subtotal + taxAmount;

            return new AddMobileWorkOrderLaborResponse
            {
                Labor = new LaborDto
                {
                    Id = $"l{labor.Id}",
                    Description = labor.OperationName,
                    Hours = labor.DurationHours ?? 0,
                    HourlyRate = labor.HourlyRate,
                    Total = labor.TotalAmount
                },
                UpdatedCosts = new UpdatedCostsDto
                {
                    PartsSubtotal = partsSubtotal,
                    LaborSubtotal = laborSubtotal,
                    TaxAmount = taxAmount,
                    Total = total
                }
            };
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return 0; // System user
        }
    }
}
