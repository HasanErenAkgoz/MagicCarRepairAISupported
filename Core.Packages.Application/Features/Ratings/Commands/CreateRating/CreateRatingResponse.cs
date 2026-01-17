using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.CreateRating
{
    public class CreateRatingResponse
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int Rating { get; set; }
        public decimal AverageRating { get; set; }
        public RatingStatus Status { get; set; }
    }
}
