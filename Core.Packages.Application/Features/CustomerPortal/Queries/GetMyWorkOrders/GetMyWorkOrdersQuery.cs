using MediatR;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrders
{
    public class GetMyWorkOrdersQuery : IRequest<List<GetMyWorkOrdersResponse>>
    {
        public bool? ActiveOnly { get; set; }
    }
}






