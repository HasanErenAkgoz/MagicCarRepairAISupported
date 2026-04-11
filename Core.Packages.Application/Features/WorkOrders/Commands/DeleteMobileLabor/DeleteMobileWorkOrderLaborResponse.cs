using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.DeleteMobileLabor
{
    /// <summary>
    /// Mobil uygulama için iş emri işçilik silme response'u
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class DeleteMobileWorkOrderLaborResponse
    {
        public UpdatedCostsDto? UpdatedCosts { get; set; }
    }
}
