using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetRecentActivities
{
    public class GetRecentActivitiesQuery : IRequest<List<GetRecentActivitiesResponse>>
    {
        public int Count { get; set; } = 10;
    }
}

