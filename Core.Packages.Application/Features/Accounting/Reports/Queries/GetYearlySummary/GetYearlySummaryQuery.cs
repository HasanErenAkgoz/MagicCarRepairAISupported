using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetYearlySummary
{
    public class GetYearlySummaryQuery : IRequest<GetYearlySummaryResponse>
    {
        public int Year { get; set; }
    }
}
