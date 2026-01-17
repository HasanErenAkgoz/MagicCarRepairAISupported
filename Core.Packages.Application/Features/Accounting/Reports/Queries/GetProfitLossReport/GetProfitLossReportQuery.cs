using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetProfitLossReport
{
    public class GetProfitLossReportQuery : IRequest<GetProfitLossReportResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

