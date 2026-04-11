using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart
{
    /// <summary>
    /// Mobil uygulama için iş emri parça ekleme command'ı
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class AddMobileWorkOrderPartCommand : IRequest<AddMobileWorkOrderPartResponse>
    {
        public string WorkOrderId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
