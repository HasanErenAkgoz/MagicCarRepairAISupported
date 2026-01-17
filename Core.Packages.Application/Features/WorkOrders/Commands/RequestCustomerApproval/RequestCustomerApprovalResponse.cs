namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RequestCustomerApproval
{
    public class RequestCustomerApprovalResponse
    {
        public int WorkOrderId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
