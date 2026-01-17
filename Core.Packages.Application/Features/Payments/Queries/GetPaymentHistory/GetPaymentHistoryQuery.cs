using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Payments.Queries.GetPaymentHistory
{
    /// <summary>
    /// Ödeme geçmişi sorgusu
    /// </summary>
    public class GetPaymentHistoryQuery : IRequest<IDataResult<List<GetPaymentHistoryResponse>>>
    {
        public int? CustomerId { get; set; }
        public int? InvoiceId { get; set; }
        public int? WorkOrderId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

