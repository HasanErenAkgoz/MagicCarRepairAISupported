using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AutoOrders.Commands.Approve
{
    public class ApproveAutoOrderCommand : IRequest<IResult>
    {
        public int AutoOrderId { get; set; }
        public int UserId { get; set; }
    }
}

