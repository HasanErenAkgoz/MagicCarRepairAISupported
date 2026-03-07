using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommand : IRequest<IDataResult<CreateSubscriptionResponse>>
    {
        public int ClientId { get; set; }
        public SubscriptionPlan Plan { get; set; }
        public string PaymentPeriod { get; set; } = "Monthly"; // Monthly or Yearly
        public bool AutoRenew { get; set; } = true;
    }

    public class CreateSubscriptionResponse
    {
        public int SubscriptionId { get; set; }
        public SubscriptionPlan Plan { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
