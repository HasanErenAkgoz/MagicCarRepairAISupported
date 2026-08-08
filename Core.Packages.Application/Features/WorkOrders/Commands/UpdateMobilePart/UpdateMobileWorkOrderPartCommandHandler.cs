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

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobilePart
{
    public class UpdateMobileWorkOrderPartCommandHandler : IRequestHandler<UpdateMobileWorkOrderPartCommand, UpdateMobileWorkOrderPartResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderItem, int> _workOrderItemRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateMobileWorkOrderPartCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<WorkOrderItem, int> workOrderItemRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderItemRepository = workOrderItemRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateMobileWorkOrderPartResponse> Handle(UpdateMobileWorkOrderPartCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.WorkOrderId, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.WorkOrderId });
            }

            if (!int.TryParse(request.PartId.Replace("p", ""), out var partId))
            {
                throw new DomainException("INVALID_PART_ID", new { Id = request.PartId });
            }

            var clientId = _tenantService.GetRequiredClientId();

            // WorkOrder'ı bul
            var workOrder = await _workOrderRepository.Query()
                .Include(wo => wo.Items)
                .Include(wo => wo.Labors)
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId && wo.ClientId == clientId, cancellationToken);

            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { Id = request.WorkOrderId });
            }

            // İş emri teslim edilmiş veya iptal edilmişse parça güncellenemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_UPDATE_PART", new { Status = workOrder.Status.ToString() });
            }

            // Item'ı bul
            var item = await _workOrderItemRepository.GetByIdAsync(partId);
            if (item == null || item.WorkOrderId != workOrderId || item.ClientId != clientId || item.ItemType != WorkOrderItemType.Part)
            {
                throw new DomainException("PART_NOT_FOUND", new { PartId = request.PartId });
            }

            // Validation
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new DomainException("PART_NAME_REQUIRED", new { Message = "Parça adı boş olamaz." });
            }

            if (request.Quantity <= 0)
            {
                throw new DomainException("INVALID_QUANTITY", new { Message = "Miktar 0'dan büyük olmalıdır." });
            }

            if (request.UnitPrice < 0)
            {
                throw new DomainException("INVALID_UNIT_PRICE", new { Message = "Birim fiyat negatif olamaz." });
            }

            // Item'ı güncelle
            item.Description = request.Name;
            item.Quantity = request.Quantity;
            item.UnitPrice = request.UnitPrice;
            item.ModifiedDate = DateTime.UtcNow;
            item.ModifiedBy = GetCurrentUserId();

            // TotalAmount hesapla
            item.CalculateTotal();

            _workOrderItemRepository.Update(item);

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

            return new UpdateMobileWorkOrderPartResponse
            {
                Part = new PartDto
                {
                    Id = $"p{item.Id}",
                    Name = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Quantity * item.UnitPrice
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
