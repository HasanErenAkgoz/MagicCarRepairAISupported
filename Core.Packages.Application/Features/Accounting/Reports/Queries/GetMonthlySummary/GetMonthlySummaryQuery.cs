using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetMonthlySummary
{
    public class GetMonthlySummaryQuery : IRequest<GetMonthlySummaryResponse>
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
