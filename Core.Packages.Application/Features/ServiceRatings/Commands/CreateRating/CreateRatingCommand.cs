using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ServiceRatings.Commands.CreateRating
{
    public class CreateRatingCommand : IRequest<IDataResult<CreateRatingResponse>>
    {
        public int WorkOrderId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; } // 1-5
        public int ServiceQuality { get; set; } // 1-5
        public int PriceValue { get; set; } // 1-5
        public int OnTimeDelivery { get; set; } // 1-5
        public int StaffBehavior { get; set; } // 1-5
        public string? Comment { get; set; }
        public List<string>? Photos { get; set; }
    }

    public class CreateRatingResponse
    {
        public int RatingId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
