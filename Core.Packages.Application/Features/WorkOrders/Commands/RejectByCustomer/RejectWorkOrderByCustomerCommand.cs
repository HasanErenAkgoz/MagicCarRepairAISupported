using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RejectByCustomer
{
    public class RejectWorkOrderByCustomerCommand : IRequest<RejectWorkOrderByCustomerResponse>
    {
        public int WorkOrderId { get; set; }
        public string RejectionReason { get; set; } = string.Empty;
    }
}
