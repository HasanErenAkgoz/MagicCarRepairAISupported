using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobileLabor
{
    /// <summary>
    /// Mobil uygulama için iş emri işçilik güncelleme command'ı
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class UpdateMobileWorkOrderLaborCommand : IRequest<UpdateMobileWorkOrderLaborResponse>
    {
        public string WorkOrderId { get; set; } = string.Empty;
        public string LaborId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Hours { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
