using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByClient
{
    public class GetRatingsByClientResponse
    {
        public List<ServiceRatingDto> Ratings { get; set; } = new();
        public int TotalCount { get; set; }
        public decimal AverageRating { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class ServiceRatingDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string VehicleInfo { get; set; } = string.Empty;
        public int Rating { get; set; }
        public decimal AverageRating { get; set; }
        public int ServiceQuality { get; set; }
        public int PriceValue { get; set; }
        public int OnTimeDelivery { get; set; }
        public int StaffBehavior { get; set; }
        public string? Comment { get; set; }
        public string? ServiceReply { get; set; }
        public RatingStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
