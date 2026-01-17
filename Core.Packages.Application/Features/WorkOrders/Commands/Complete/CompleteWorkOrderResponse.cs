using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Complete
{
    public class CompleteWorkOrderResponse
    {
        public int WorkOrderId { get; set; }
        public WorkOrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}

