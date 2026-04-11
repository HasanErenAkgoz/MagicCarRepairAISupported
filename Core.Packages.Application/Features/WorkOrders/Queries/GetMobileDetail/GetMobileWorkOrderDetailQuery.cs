using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail
{
    /// <summary>
    /// Mobil uygulama için iş emri detay query'si
    /// Doküman: WORK_ORDER_REQUIREMENTS.md
    /// </summary>
    public class GetMobileWorkOrderDetailQuery : IRequest<GetMobileWorkOrderDetailResponse>
    {
        public string Id { get; set; } = string.Empty;
    }
}
