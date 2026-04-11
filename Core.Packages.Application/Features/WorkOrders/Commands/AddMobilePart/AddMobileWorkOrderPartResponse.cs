namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart
{
    /// <summary>
    /// Mobil uygulama için iş emri parça ekleme response'u
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class AddMobileWorkOrderPartResponse
    {
        public PartDto Part { get; set; } = new();
        public UpdatedCostsDto? UpdatedCosts { get; set; }
    }

    public class PartDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }

    public class UpdatedCostsDto
    {
        public decimal PartsSubtotal { get; set; }
        public decimal LaborSubtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
    }
}
