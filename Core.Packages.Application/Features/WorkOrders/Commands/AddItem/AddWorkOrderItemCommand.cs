using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddItem
{
    public class AddWorkOrderItemCommand : IRequest<AddWorkOrderItemResponse>
    {
        public int WorkOrderId { get; set; }
        public WorkOrderItemType ItemType { get; set; }
        public int? PartId { get; set; } // ItemType = Part ise zorunlu
        public string Description { get; set; }
        public decimal Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; } = 0;
        public decimal TaxRate { get; set; } = 20; // %20 KDV
        public PartBrandType? BrandType { get; set; }
        public int? WarrantyMonths { get; set; }
        public string? Notes { get; set; }
    }
}

