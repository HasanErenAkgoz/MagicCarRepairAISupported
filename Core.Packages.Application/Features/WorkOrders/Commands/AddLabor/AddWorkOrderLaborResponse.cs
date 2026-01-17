namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddLabor
{
    public class AddWorkOrderLaborResponse
    {
        public int LaborId { get; set; }
        public int WorkOrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal WorkOrderTotalAmount { get; set; }
    }
}

