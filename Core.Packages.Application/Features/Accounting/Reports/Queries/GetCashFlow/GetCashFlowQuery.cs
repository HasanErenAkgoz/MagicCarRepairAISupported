using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetCashFlow
{
    public class GetCashFlowQuery : IRequest<GetCashFlowResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? GroupByDays { get; set; } // 1, 7, 30 (günlük, haftalık, aylık)
    }
}
