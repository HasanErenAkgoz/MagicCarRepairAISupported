using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicReviews
{
    public class GetPublicReviewsQuery : IRequest<IDataResult<List<GetPublicReviewsResponse>>>
    {
        public int ClientId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
