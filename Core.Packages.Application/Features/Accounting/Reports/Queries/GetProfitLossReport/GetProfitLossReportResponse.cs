namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetProfitLossReport
{
    public class GetProfitLossReportResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetProfit { get; set; }
        public decimal ProfitMargin { get; set; } // Kar marjı (%)
        public List<MonthlySummary> MonthlySummaries { get; set; } = new();
    }

    public class MonthlySummary
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Profit { get; set; }
    }
}

