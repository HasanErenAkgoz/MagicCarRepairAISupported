using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetIncomeReport
{
    public class GetIncomeReportResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
        public Dictionary<IncomeType, decimal> AmountByType { get; set; } = new();
        public Dictionary<PaymentMethod, decimal> AmountByPaymentMethod { get; set; } = new();
        public List<DailyIncomeSummary> DailySummaries { get; set; } = new();
    }

    public class DailyIncomeSummary
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Count { get; set; }
    }
}

