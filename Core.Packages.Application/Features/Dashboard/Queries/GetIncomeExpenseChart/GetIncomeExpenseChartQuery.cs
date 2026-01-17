using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetIncomeExpenseChart
{
    public class GetIncomeExpenseChartQuery : IRequest<List<GetIncomeExpenseChartResponse>>
    {
        public DateTime StartDate { get; set; } = DateTime.UtcNow.AddMonths(-6);
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
        public ChartGroupBy GroupBy { get; set; } = ChartGroupBy.Month;
    }

    public enum ChartGroupBy
    {
        Day = 1,
        Week = 2,
        Month = 3,
        Year = 4
    }
}

