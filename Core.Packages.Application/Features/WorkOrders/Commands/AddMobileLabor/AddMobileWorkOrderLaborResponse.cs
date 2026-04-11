using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobileLabor
{
    /// <summary>
    /// Mobil uygulama için iş emri işçilik ekleme response'u
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class AddMobileWorkOrderLaborResponse
    {
        public LaborDto Labor { get; set; } = new();
        public UpdatedCostsDto? UpdatedCosts { get; set; }
    }

    public class LaborDto
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Total { get; set; }
    }
}
