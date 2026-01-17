using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.FinancialCharts
{
    public class GetFinancialChartsQuery : IRequest<GetFinancialChartsResponse>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string GroupBy { get; set; } = "month"; // month, week, day
    }
}

