namespace MagicCarRepairAISupported.Application.Features.AutoOrders.Commands.Create
{
    public class CreateAutoOrderResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
    }
}

