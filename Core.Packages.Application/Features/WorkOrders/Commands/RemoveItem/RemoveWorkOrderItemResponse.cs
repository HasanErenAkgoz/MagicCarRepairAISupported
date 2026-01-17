namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RemoveItem
{
    public class RemoveWorkOrderItemResponse
    {
        public int WorkOrderId { get; set; }
        public int ItemId { get; set; }
        public decimal WorkOrderTotalAmount { get; set; }
        public string Message { get; set; }
    }
}

