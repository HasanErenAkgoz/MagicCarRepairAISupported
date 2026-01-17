using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.WorkOrderStatistics
{
    public class GetWorkOrderStatisticsResponse
    {
        public int TotalWorkOrders { get; set; }
        public int CompletedWorkOrders { get; set; }
        public double AverageCompletionDays { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageRevenue { get; set; }
        public List<EmployeePerformanceItem> EmployeePerformance { get; set; } = new();
        public List<StatusDistributionItem> StatusDistribution { get; set; } = new();
        public List<TrendItem> MonthlyTrend { get; set; } = new();
        public List<TrendItem> WeeklyTrend { get; set; } = new();
    }

    public class EmployeePerformanceItem
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int CompletedCount { get; set; }
        public double AverageCompletionDays { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class StatusDistributionItem
    {
        public WorkOrderStatus Status { get; set; }
        public string StatusName { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

    public class TrendItem
    {
        public string Period { get; set; }
        public int Count { get; set; }
        public decimal TotalRevenue { get; set; }
        public int CompletedCount { get; set; }
    }
}

