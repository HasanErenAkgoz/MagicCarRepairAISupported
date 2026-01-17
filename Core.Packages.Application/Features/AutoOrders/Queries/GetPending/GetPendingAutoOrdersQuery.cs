using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AutoOrders.Queries.GetPending
{
    public class GetPendingAutoOrdersQuery : IRequest<IDataResult<List<GetPendingAutoOrdersResponse>>>
    {
    }
}

