namespace MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeEmployeePerformance
{
    public class AnalyzeEmployeePerformanceResponse
    {
        public List<EmployeePerformanceAnalysisDto> Analyses { get; set; } = new();
        public string Summary { get; set; } = string.Empty;
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
    }

    public class EmployeePerformanceAnalysisDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string PerformanceLevel { get; set; } = string.Empty;
        public int PerformanceScore { get; set; }
        public int TotalAssignedWorkOrders { get; set; }
        public int CompletedWorkOrders { get; set; }
        public double TotalWorkHours { get; set; }
        public double AverageCompletionDays { get; set; }
        public decimal TotalRevenueContribution { get; set; }
        public double AverageCustomerSatisfaction { get; set; }
        public double OnTimeDeliveryRate { get; set; }
        public int EfficiencyScore { get; set; }
        public string Trend { get; set; } = string.Empty;
        public List<string> Strengths { get; set; } = new();
        public List<string> ImprovementAreas { get; set; } = new();
        public List<string> Recommendations { get; set; } = new();
        public string Explanation { get; set; } = string.Empty;
        public int ConfidenceScore { get; set; }
    }
}
