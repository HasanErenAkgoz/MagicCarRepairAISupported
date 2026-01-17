using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetExpenseReport
{
    public class GetExpenseReportResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
        public Dictionary<ExpenseType, decimal> AmountByType { get; set; } = new();
        public Dictionary<PaymentMethod, decimal> AmountByPaymentMethod { get; set; } = new();
        public List<DailyExpenseSummary> DailySummaries { get; set; } = new();
    }

    public class DailyExpenseSummary
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Count { get; set; }
    }
}

