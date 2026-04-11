using MediatR;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.SubmitWorkOrderReview
{
    public class SubmitWorkOrderReviewCommand : IRequest<SubmitWorkOrderReviewResponse>
    {
        public int WorkOrderId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
