using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetTimeline
{
    public class GetWorkOrderTimelineQuery : IRequest<List<GetWorkOrderTimelineResponse>>
    {
        public int WorkOrderId { get; set; }
    }
}

