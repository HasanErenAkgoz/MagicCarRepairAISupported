using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicReviews
{
    public class GetPublicReviewsQueryHandler : IRequestHandler<GetPublicReviewsQuery, IDataResult<List<GetPublicReviewsResponse>>>
    {
        private readonly IServiceRatingRepository _ratingRepository;

        public GetPublicReviewsQueryHandler(IServiceRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IDataResult<List<GetPublicReviewsResponse>>> Handle(GetPublicReviewsQuery request, CancellationToken cancellationToken)
        {
            var allRatings = await _ratingRepository.GetByClientIdAsync(request.ClientId, cancellationToken);
            var ratings = allRatings.Where(r => r.Status == RatingStatus.Approved).ToList();

            var responses = new List<GetPublicReviewsResponse>();
            var customerCounter = 1;

            foreach (var rating in ratings.OrderByDescending(r => r.CreatedDate))
            {
                // Anonymize customer name
                var customerName = $"Müşteri {customerCounter++}";

                var response = new GetPublicReviewsResponse
                {
                    Id = rating.Id,
                    Rating = rating.Rating,
                    Comment = rating.Comment,
                    CreatedDate = rating.CreatedDate ?? DateTime.UtcNow,
                    CustomerName = customerName,
                    WorkOrderNumber = rating.WorkOrder?.WorkOrderNumber
                };

                responses.Add(response);
            }

            // Pagination
            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 10;
            var pagedResults = responses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new SuccessDataResult<List<GetPublicReviewsResponse>>(pagedResults);
        }
    }
}
