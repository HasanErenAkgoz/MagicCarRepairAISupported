using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Update
{
    public class UpdateWorkOrderResponse
    {
        public int Id { get; set; }
        public string WorkOrderNumber { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public WorkOrderPriority Priority { get; set; }
        public long? Kilometers { get; set; }
        public int? FuelLevel { get; set; }
        public string? CustomerComplaints { get; set; }
        public string? SpecialRequests { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public string? AssignedEmployeeName { get; set; }
        public string? Notes { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
