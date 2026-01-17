using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Create
{
    public class CreateWorkOrderResponse
    {
        public int Id { get; set; }
        public string WorkOrderNumber { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public WorkOrderStatus Status { get; set; }
        public WorkOrderPriority Priority { get; set; }
        public long? Kilometers { get; set; }
        public int? FuelLevel { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

