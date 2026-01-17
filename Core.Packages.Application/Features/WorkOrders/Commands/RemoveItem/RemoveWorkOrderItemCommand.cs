using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RemoveItem
{
    public class RemoveWorkOrderItemCommand : IRequest<RemoveWorkOrderItemResponse>
    {
        public int WorkOrderId { get; set; }
        public int ItemId { get; set; }
    }
}

