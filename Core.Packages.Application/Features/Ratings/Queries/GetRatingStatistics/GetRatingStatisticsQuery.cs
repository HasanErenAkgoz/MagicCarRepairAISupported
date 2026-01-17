using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingStatistics
{
    public class GetRatingStatisticsQuery : IRequest<GetRatingStatisticsResponse>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
