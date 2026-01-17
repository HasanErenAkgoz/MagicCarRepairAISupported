namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.ApproveByCustomer
{
    public class ApproveWorkOrderByCustomerResponse
    {
        public int WorkOrderId { get; set; }
        public bool Success { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
