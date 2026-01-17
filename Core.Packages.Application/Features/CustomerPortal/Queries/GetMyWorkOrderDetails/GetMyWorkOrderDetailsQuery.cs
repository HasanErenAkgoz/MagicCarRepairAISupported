using MediatR;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrderDetails
{
    public class GetMyWorkOrderDetailsQuery : IRequest<GetMyWorkOrderDetailsResponse>
    {
        public int WorkOrderId { get; set; }
    }
}






