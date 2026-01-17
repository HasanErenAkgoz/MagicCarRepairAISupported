using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByWorkOrder
{
    public class GetRatingsByWorkOrderQuery : IRequest<GetRatingsByWorkOrderResponse>
    {
        public int WorkOrderId { get; set; }
    }
}
