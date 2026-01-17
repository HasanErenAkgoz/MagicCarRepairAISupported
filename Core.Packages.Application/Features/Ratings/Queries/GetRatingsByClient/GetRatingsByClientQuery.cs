using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByClient
{
    public class GetRatingsByClientQuery : IRequest<GetRatingsByClientResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public bool? OnlyApproved { get; set; } = true;
    }
}
