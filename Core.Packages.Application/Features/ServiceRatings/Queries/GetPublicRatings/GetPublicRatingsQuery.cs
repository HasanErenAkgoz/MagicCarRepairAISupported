using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ServiceRatings.Queries.GetPublicRatings
{
    public class GetPublicRatingsQuery : IRequest<IDataResult<GetPublicRatingsResponse>>
    {
        public int ClientId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int? MinRating { get; set; }
    }

    public class GetPublicRatingsResponse
    {
        public List<PublicRatingDto> Ratings { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public decimal AverageRating { get; set; }
        public RatingStatistics Statistics { get; set; } = new();
    }

    public class PublicRatingDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public int ServiceQuality { get; set; }
        public int PriceValue { get; set; }
        public int OnTimeDelivery { get; set; }
        public int StaffBehavior { get; set; }
        public string? Comment { get; set; }
        public List<string>? Photos { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ServiceReply { get; set; }
        public DateTime? ServiceReplyDate { get; set; }
    }

    public class RatingStatistics
    {
        public int TotalRatings { get; set; }
        public int FiveStarCount { get; set; }
        public int FourStarCount { get; set; }
        public int ThreeStarCount { get; set; }
        public int TwoStarCount { get; set; }
        public int OneStarCount { get; set; }
    }
}
