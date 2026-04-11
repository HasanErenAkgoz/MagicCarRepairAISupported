using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.DeleteMobilePart
{
    /// <summary>
    /// Mobil uygulama için iş emri parça silme response'u
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class DeleteMobileWorkOrderPartResponse
    {
        public UpdatedCostsDto? UpdatedCosts { get; set; }
    }
}
