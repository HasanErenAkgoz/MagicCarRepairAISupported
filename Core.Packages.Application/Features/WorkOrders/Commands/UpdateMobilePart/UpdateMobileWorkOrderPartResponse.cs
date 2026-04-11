using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobilePart
{
    /// <summary>
    /// Mobil uygulama için iş emri parça güncelleme response'u
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class UpdateMobileWorkOrderPartResponse
    {
        public PartDto Part { get; set; } = new();
        public UpdatedCostsDto? UpdatedCosts { get; set; }
    }
}
