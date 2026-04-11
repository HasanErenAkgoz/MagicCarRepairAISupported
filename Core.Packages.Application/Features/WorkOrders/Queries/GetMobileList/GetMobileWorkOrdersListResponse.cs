namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileList
{
    /// <summary>
    /// Mobil uygulama için iş emri listesi response'u
    /// Doküman: WORK_ORDER_REQUIREMENTS.md
    /// </summary>
    public class GetMobileWorkOrdersListResponse
    {
        public List<WorkOrderListItem> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public class WorkOrderListItem
    {
        public string Id { get; set; } = string.Empty;
        public string OrderNo { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string VehiclePlate { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // pending, inProgress, completed, cancelled
        public string Date { get; set; } = string.Empty; // ISO 8601: YYYY-MM-DD
        public decimal Amount { get; set; }
        public string? TechnicianName { get; set; }
    }
}
