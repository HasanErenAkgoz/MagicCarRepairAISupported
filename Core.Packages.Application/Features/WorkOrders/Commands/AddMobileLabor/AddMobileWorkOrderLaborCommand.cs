using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobileLabor
{
    /// <summary>
    /// Mobil uygulama için iş emri işçilik ekleme command'ı
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class AddMobileWorkOrderLaborCommand : IRequest<AddMobileWorkOrderLaborResponse>
    {
        public string WorkOrderId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Hours { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
