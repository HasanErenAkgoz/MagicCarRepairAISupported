using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.DeleteMobilePart
{
    /// <summary>
    /// Mobil uygulama için iş emri parça silme command'ı
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class DeleteMobileWorkOrderPartCommand : IRequest<DeleteMobileWorkOrderPartResponse>
    {
        public string WorkOrderId { get; set; } = string.Empty;
        public string PartId { get; set; } = string.Empty;
    }
}
