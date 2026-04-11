using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobilePart
{
    /// <summary>
    /// Mobil uygulama için iş emri parça güncelleme command'ı
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class UpdateMobileWorkOrderPartCommand : IRequest<UpdateMobileWorkOrderPartResponse>
    {
        public string WorkOrderId { get; set; } = string.Empty;
        public string PartId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
