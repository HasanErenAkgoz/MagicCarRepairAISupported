using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.ModerateRating
{
    public class ModerateRatingResponse
    {
        public int Id { get; set; }
        public RatingStatus Status { get; set; }
        public string? RejectionReason { get; set; }
    }
}
