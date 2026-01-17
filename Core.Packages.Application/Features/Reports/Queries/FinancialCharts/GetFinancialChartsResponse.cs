using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.FinancialCharts
{
    public class GetFinancialChartsResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<IncomeExpenseItem> IncomeExpenseComparison { get; set; } = new();
        public List<ProfitTrendItem> ProfitTrend { get; set; } = new();
        public List<CategoryExpenseItem> CategoryExpenses { get; set; } = new();
        public List<PaymentMethodDistributionItem> PaymentMethodDistribution { get; set; } = new();
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalInvoiceAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
    }

    public class IncomeExpenseItem
    {
        public string Period { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Profit { get; set; }
    }

    public class ProfitTrendItem
    {
        public string Period { get; set; }
        public decimal Profit { get; set; }
    }

    public class CategoryExpenseItem
    {
        public ExpenseType Category { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public int Count { get; set; }
    }

    public class PaymentMethodDistributionItem
    {
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
        public double Percentage { get; set; }
    }
}

