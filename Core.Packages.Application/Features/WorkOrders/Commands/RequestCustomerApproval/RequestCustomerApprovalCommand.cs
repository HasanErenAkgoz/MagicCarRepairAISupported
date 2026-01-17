using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RequestCustomerApproval
{
    public class RequestCustomerApprovalCommand : IRequest<RequestCustomerApprovalResponse>
    {
        public int WorkOrderId { get; set; }
        public string? Message { get; set; }
    }
}
