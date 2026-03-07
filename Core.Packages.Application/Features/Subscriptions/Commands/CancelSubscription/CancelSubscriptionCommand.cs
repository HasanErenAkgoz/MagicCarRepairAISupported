using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.CancelSubscription
{
    public class CancelSubscriptionCommand : IRequest<IDataResult<CancelSubscriptionResponse>>
    {
        public int ClientId { get; set; }
        public string? Reason { get; set; }
    }

    public class CancelSubscriptionResponse
    {
        public int SubscriptionId { get; set; }
        public DateTime CancelledDate { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
