using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateStatus
{
    public class UpdateWorkOrderStatusResponse
    {
        public int WorkOrderId { get; set; }
        public WorkOrderStatus OldStatus { get; set; }
        public WorkOrderStatus NewStatus { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

