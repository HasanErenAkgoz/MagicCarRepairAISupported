namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetIncomeExpenseChart
{
    public class GetIncomeExpenseChartResponse
    {
        public string Period { get; set; } // "2024-01", "2024-W01", vb.
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Profit { get; set; }
    }
}

