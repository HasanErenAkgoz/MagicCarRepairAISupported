namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePartStock
{
    public class UpdatePartStockResponse
    {
        public int PartId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public string? Location { get; set; }
        public bool IsLowStock { get; set; }
    }
}

