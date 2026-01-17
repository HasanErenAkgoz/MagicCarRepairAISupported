namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddItem
{
    public class AddWorkOrderItemResponse
    {
        public int ItemId { get; set; }
        public int WorkOrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal WorkOrderTotalAmount { get; set; }
    }
}

