using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetExpiringPolicies
{
    public class GetExpiringPoliciesQuery : IRequest<GetExpiringPoliciesResponse>
    {
        public int DaysBeforeExpiration { get; set; } = 30;
    }
}

