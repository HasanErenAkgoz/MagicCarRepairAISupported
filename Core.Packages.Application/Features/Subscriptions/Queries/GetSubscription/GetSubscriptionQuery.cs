using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQuery : IRequest<IDataResult<GetSubscriptionResponse>>
    {
        public int ClientId { get; set; }
    }

    public class GetSubscriptionResponse
    {
        public int SubscriptionId { get; set; }
        public string Plan { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal? YearlyPrice { get; set; }
        public int MaxWorkOrders { get; set; }
        public int MaxUsers { get; set; }
        public int MaxAIAnalyses { get; set; }
        public List<string> Features { get; set; } = new List<string>();
        public bool AutoRenew { get; set; }
        public bool IsActive { get; set; }
        public DateTime? NextPaymentDate { get; set; }
    }
}
