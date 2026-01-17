namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetTopCustomers
{
    public class GetTopCustomersResponse
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int WorkOrderCount { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime? LastVisitDate { get; set; }
    }
}

