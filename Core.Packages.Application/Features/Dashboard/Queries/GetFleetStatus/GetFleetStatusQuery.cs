using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetFleetStatus
{
    public class GetFleetStatusQuery : IRequest<IDataResult<GetFleetStatusResponse>>
    {
    }
}
