using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobileLabor;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobileLabor
{
    /// <summary>
    /// Mobil uygulama için iş emri işçilik güncelleme response'u
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class UpdateMobileWorkOrderLaborResponse
    {
        public LaborDto Labor { get; set; } = new();
        public UpdatedCostsDto? UpdatedCosts { get; set; }
    }
}
