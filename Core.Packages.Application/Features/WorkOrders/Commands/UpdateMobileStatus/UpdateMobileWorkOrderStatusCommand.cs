using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobileStatus
{
    /// <summary>
    /// Mobil uygulama için iş emri durum güncelleme command'ı
    /// Doküman: WORK_ORDER_REQUIREMENTS.md
    /// </summary>
    public class UpdateMobileWorkOrderStatusCommand : IRequest<UpdateMobileWorkOrderStatusResponse>
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // pending, inProgress, completed, cancelled
    }
}
