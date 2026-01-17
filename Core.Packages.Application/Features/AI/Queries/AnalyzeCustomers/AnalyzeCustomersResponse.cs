namespace MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeCustomers
{
    public class AnalyzeCustomersResponse
    {
        public List<CustomerAnalysisDto> Analyses { get; set; } = new();
        public string Summary { get; set; } = string.Empty;
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
    }

    public class CustomerAnalysisDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerSegment { get; set; } = string.Empty;
        public string CustomerValue { get; set; } = string.Empty;
        public int TotalWorkOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageWorkOrderAmount { get; set; }
        public double AverageRating { get; set; }
        public int TotalVehicles { get; set; }
        public DateTime? LastVisitDate { get; set; }
        public int DaysSinceLastVisit { get; set; }
        public string Trend { get; set; } = string.Empty;
        public List<string> RiskFactors { get; set; } = new();
        public List<string> Recommendations { get; set; } = new();
        public string PaymentBehavior { get; set; } = string.Empty;
        public double AveragePaymentDelayDays { get; set; }
        public string Explanation { get; set; } = string.Empty;
        public int ConfidenceScore { get; set; }
    }
}
