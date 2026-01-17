using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetActive
{
    public class GetActiveWorkOrdersQuery : IRequest<List<GetActiveWorkOrdersResponse>>
    {
    }
}

