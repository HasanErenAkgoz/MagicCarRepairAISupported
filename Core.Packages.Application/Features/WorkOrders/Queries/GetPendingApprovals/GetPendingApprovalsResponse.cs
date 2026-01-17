namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetPendingApprovals
{
    public class GetPendingApprovalsResponse
    {
        public List<PendingApprovalDto> WorkOrders { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class PendingApprovalDto
    {
        public int Id { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleLicensePlate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
