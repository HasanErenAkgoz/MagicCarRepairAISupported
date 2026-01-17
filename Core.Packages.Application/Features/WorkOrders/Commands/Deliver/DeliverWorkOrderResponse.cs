using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Deliver
{
    public class DeliverWorkOrderResponse
    {
        public int WorkOrderId { get; set; }
        public WorkOrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime DeliveredDate { get; set; }
    }
}

