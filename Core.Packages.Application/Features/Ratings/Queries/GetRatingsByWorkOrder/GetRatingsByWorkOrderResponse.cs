namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByWorkOrder
{
    public class GetRatingsByWorkOrderResponse
    {
        public int WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; }
        public List<RatingItem> Ratings { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class RatingItem
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int OverallRating { get; set; }
        public int ServiceQuality { get; set; }
        public int PriceValue { get; set; }
        public int OnTimeDelivery { get; set; }
        public int StaffBehavior { get; set; }
        public string? Comment { get; set; }
        public string? Reply { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ReplyDate { get; set; }
    }
}
