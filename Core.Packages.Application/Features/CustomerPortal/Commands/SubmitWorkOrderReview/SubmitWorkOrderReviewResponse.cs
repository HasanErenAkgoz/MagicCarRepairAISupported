namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.SubmitWorkOrderReview
{
    public class SubmitWorkOrderReviewResponse
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
