using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.UpgradeSubscription
{
    public class UpgradeSubscriptionCommand : IRequest<IDataResult<UpgradeSubscriptionResponse>>
    {
        public int ClientId { get; set; }
        public SubscriptionPlan NewPlan { get; set; }
        public string PaymentPeriod { get; set; } = "Monthly";
    }

    public class UpgradeSubscriptionResponse
    {
        public int SubscriptionId { get; set; }
        public SubscriptionPlan OldPlan { get; set; }
        public SubscriptionPlan NewPlan { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
