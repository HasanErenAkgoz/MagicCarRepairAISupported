using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobileStatus
{
    /// <summary>
    /// Mobil uygulama için iş emri durum güncelleme response'u
    /// Doküman: WORK_ORDER_REQUIREMENTS.md
    /// Tam detay response döner
    /// </summary>
    public class UpdateMobileWorkOrderStatusResponse
    {
        public GetMobileWorkOrderDetailResponse Data { get; set; } = new();
    }
}
