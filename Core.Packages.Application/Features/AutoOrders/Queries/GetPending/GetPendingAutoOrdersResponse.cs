using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.AutoOrders.Queries.GetPending
{
    public class GetPendingAutoOrdersResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int PartId { get; set; }
        public string PartCode { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public AutoOrderStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

