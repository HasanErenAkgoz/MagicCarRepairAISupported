using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetPendingApprovals
{
    public class GetPendingApprovalsQuery : IRequest<GetPendingApprovalsResponse>
    {
        public int? CustomerId { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; } = 50;
    }
}
