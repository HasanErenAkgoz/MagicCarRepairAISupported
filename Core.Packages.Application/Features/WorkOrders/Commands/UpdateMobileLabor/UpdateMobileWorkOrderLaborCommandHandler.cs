using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobileLabor;
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

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobileLabor
{
    public class UpdateMobileWorkOrderLaborCommandHandler : IRequestHandler<UpdateMobileWorkOrderLaborCommand, UpdateMobileWorkOrderLaborResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderLabor, int> _workOrderLaborRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateMobileWorkOrderLaborCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<WorkOrderLabor, int> workOrderLaborRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderLaborRepository = workOrderLaborRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateMobileWorkOrderLaborResponse> Handle(UpdateMobileWorkOrderLaborCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.WorkOrderId, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.WorkOrderId });
            }

            if (!int.TryParse(request.LaborId.Replace("l", ""), out var laborId))
            {
                throw new DomainException("INVALID_LABOR_ID", new { Id = request.LaborId });
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

            // İş emri teslim edilmiş veya iptal edilmişse işçilik güncellenemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_UPDATE_LABOR", new { Status = workOrder.Status.ToString() });
            }

            // Labor'ı bul
            var labor = await _workOrderLaborRepository.GetByIdAsync(laborId);
            if (labor == null || labor.WorkOrderId != workOrderId || labor.ClientId != clientId)
            {
                throw new DomainException("LABOR_NOT_FOUND", new { LaborId = request.LaborId });
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

            // Labor'ı güncelle
            labor.OperationName = request.Description;
            labor.DurationHours = request.Hours;
            labor.HourlyRate = request.HourlyRate;
            labor.ModifiedDate = DateTime.UtcNow;
            labor.ModifiedBy = GetCurrentUserId();

            // TotalAmount hesapla
            labor.CalculateTotal();

            _workOrderLaborRepository.Update(labor);

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

            return new UpdateMobileWorkOrderLaborResponse
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
