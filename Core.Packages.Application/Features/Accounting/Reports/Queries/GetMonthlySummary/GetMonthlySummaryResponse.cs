namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetMonthlySummary
{
    public class GetMonthlySummaryResponse
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetProfit { get; set; }
        public int IncomeCount { get; set; }
        public int ExpenseCount { get; set; }
        public decimal TotalSalaryPayments { get; set; }
        public int SalaryPaymentCount { get; set; }
        public decimal TotalTaxPayments { get; set; }
        public int TaxPaymentCount { get; set; }
        public MonthlyBreakdown Breakdown { get; set; } = new();
    }

    public class MonthlyBreakdown
    {
        public decimal WorkOrderIncome { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal SalaryExpenses { get; set; }
        public decimal TaxExpenses { get; set; }
        public decimal PartExpenses { get; set; }
        public decimal OtherExpenses { get; set; }
    }
}
