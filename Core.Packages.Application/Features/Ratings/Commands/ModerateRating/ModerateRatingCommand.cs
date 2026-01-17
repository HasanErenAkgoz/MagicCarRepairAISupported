using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.ModerateRating
{
    public class ModerateRatingCommand : IRequest<ModerateRatingResponse>
    {
        public int RatingId { get; set; }
        public RatingStatus Status { get; set; }
        public string? RejectionReason { get; set; }
    }
}
