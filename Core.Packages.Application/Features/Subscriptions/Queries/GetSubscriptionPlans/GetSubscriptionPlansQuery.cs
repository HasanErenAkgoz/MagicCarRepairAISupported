using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Queries.GetSubscriptionPlans
{
    public class GetSubscriptionPlansQuery : IRequest<IDataResult<List<SubscriptionPlanDto>>>
    {
    }

    public class SubscriptionPlanDto
    {
        public string Plan { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MonthlyPrice { get; set; }
        public decimal? YearlyPrice { get; set; }
        public int MaxWorkOrders { get; set; }
        public int MaxUsers { get; set; }
        public int MaxAIAnalyses { get; set; }
        public List<string> Features { get; set; } = new List<string>();
        public bool IsPopular { get; set; }
    }
}
