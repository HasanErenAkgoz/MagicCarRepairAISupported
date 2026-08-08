using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart
{
    public class AddMobileWorkOrderPartCommandHandler : IRequestHandler<AddMobileWorkOrderPartCommand, AddMobileWorkOrderPartResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderItem, int> _workOrderItemRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddMobileWorkOrderPartCommandHandler(
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

        public async Task<AddMobileWorkOrderPartResponse> Handle(AddMobileWorkOrderPartCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.WorkOrderId, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.WorkOrderId });
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

            // İş emri teslim edilmiş veya iptal edilmişse parça eklenemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_ADD_PART", new { Status = workOrder.Status.ToString() });
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

            // WorkOrderItem oluştur
            var item = new WorkOrderItem
            {
                WorkOrderId = workOrder.Id,
                ItemType = WorkOrderItemType.Part,
                Description = request.Name,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TaxRate = workOrder.TaxAmount > 0 && workOrder.SubTotal > 0
                    ? (workOrder.TaxAmount / workOrder.SubTotal) * 100
                    : 20, // Varsayılan %20
                ClientId = clientId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = GetCurrentUserId(),
                Status = Domain.Enums.Status.Active
            };

            // TotalAmount hesapla
            item.CalculateTotal();

            // Item'ı ekle
            await _workOrderItemRepository.AddAsync(item, cancellationToken);

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

            return new AddMobileWorkOrderPartResponse
            {
                Part = new PartDto
                {
                    Id = $"p{item.Id}",
                    Name = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Quantity * item.UnitPrice // KDV hariç toplam
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
