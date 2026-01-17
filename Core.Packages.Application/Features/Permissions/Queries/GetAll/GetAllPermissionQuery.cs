using MagicCarRepairAISupported.Application.Common.Attributies;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Permission.Queries.GetAll
{
    [Cache("permissions_{UserId}", 30)]
    public class GetAllPermissionQuery : IRequest<IDataResult<IEnumerable<GetPermissionResponse>>>
    {

    }
}
