namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RejectByCustomer
{
    public class RejectWorkOrderByCustomerResponse
    {
        public int WorkOrderId { get; set; }
        public bool Success { get; set; }
        public DateTime RejectionDate { get; set; }
    }
}
