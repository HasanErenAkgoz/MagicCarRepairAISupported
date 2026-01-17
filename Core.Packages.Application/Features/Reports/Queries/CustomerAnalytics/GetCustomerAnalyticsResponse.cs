namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.CustomerAnalytics
{
    public class GetCustomerAnalyticsResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CustomerAnalyticsItem> CustomerAnalytics { get; set; } = new();
        public List<SegmentDistributionItem> SegmentDistribution { get; set; } = new();
        public double RepeatRate { get; set; }
        public List<CustomerAnalyticsItem> TopCustomers { get; set; } = new();
        public double AverageLoyaltyScore { get; set; }
        public decimal AverageCLV { get; set; }
    }

    public class CustomerAnalyticsItem
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalVisits { get; set; }
        public int CompletedOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageVisitAmount { get; set; }
        public DateTime? LastVisitDate { get; set; }
        public double ReturnRate { get; set; }
        public decimal CustomerLifetimeValue { get; set; }
        public int LoyaltyScore { get; set; }
        public string Segment { get; set; }
    }

    public class SegmentDistributionItem
    {
        public string Segment { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}

