using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.CreateRating
{
    public class CreateRatingCommand : IRequest<CreateRatingResponse>
    {
        public int WorkOrderId { get; set; }
        public int Rating { get; set; } // 1-5
        public int ServiceQuality { get; set; } // 1-5
        public int PriceValue { get; set; } // 1-5
        public int OnTimeDelivery { get; set; } // 1-5
        public int StaffBehavior { get; set; } // 1-5
        public string? Comment { get; set; }
        public string? Photos { get; set; } // JSON formatında file paths
    }
}
