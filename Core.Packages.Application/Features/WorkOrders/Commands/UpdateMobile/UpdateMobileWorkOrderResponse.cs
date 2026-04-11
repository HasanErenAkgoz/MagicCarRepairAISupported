using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobile
{
    /// <summary>
    /// Mobil uygulama için iş emri güncelleme response'u
    /// Doküman: work-orders-api.md
    /// Tam detay response döner
    /// </summary>
    public class UpdateMobileWorkOrderResponse
    {
        public GetMobileWorkOrderDetailResponse Data { get; set; } = new();
    }
}
