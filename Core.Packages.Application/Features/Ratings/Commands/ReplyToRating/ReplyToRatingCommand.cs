using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.ReplyToRating
{
    public class ReplyToRatingCommand : IRequest<ReplyToRatingResponse>
    {
        public int RatingId { get; set; }
        public string ServiceReply { get; set; } = string.Empty;
    }
}
