using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetActive
{
    public class GetActiveWorkOrdersResponse
    {
        public int Id { get; set; }
        public string WorkOrderNumber { get; set; }
        public string VehicleLicensePlate { get; set; }
        public string CustomerName { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public WorkOrderStatus Status { get; set; }
        public string StatusName { get; set; }
        public WorkOrderPriority Priority { get; set; }
        public decimal TotalAmount { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public string? AssignedEmployeeName { get; set; }
    }
}

