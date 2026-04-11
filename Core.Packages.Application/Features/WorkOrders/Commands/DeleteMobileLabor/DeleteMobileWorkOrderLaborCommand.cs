using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.DeleteMobileLabor
{
    /// <summary>
    /// Mobil uygulama için iş emri işçilik silme command'ı
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class DeleteMobileWorkOrderLaborCommand : IRequest<DeleteMobileWorkOrderLaborResponse>
    {
        public string WorkOrderId { get; set; } = string.Empty;
        public string LaborId { get; set; } = string.Empty;
    }
}
