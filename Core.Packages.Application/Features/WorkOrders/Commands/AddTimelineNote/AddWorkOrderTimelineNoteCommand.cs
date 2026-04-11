using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddTimelineNote
{
    /// <summary>
    /// Mobil uygulama için iş emri timeline manuel not ekleme command'ı
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class AddWorkOrderTimelineNoteCommand : IRequest<AddWorkOrderTimelineNoteResponse>
    {
        public string WorkOrderId { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
