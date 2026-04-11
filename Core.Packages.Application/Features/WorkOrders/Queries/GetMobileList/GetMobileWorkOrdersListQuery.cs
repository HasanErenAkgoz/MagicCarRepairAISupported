using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileList
{
    /// <summary>
    /// Mobil uygulama için iş emri listesi query'si
    /// Doküman: WORK_ORDER_REQUIREMENTS.md
    /// </summary>
    public class GetMobileWorkOrdersListQuery : IRequest<GetMobileWorkOrdersListResponse>
    {
        /// <summary>
        /// Status filtresi: pending, inProgress, completed, cancelled
        /// </summary>
        public string? Status { get; set; }
        
        /// <summary>
        /// Sayfa numarası (varsayılan: 1)
        /// </summary>
        public int Page { get; set; } = 1;
        
        /// <summary>
        /// Sayfa boyutu (varsayılan: 20)
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Müşteri filtresi: belirtilirse sadece o müşterinin iş emirleri döner
        /// </summary>
        public int? CustomerId { get; set; }
    }
}
