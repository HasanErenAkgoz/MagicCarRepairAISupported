using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.CreateMobile
{
    /// <summary>
    /// Mobil uygulama için iş emri oluşturma response'u
    /// Doküman: WORK_ORDER_CREATE_REQUIREMENTS.md
    /// GetMobileWorkOrderDetailResponse formatında döner
    /// </summary>
    public class CreateMobileWorkOrderResponse
    {
        public GetMobileWorkOrderDetailResponse Data { get; set; } = new();
    }
}
