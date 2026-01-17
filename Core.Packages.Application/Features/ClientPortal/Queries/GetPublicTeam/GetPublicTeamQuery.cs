using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicTeam
{
    public class GetPublicTeamQuery : IRequest<IDataResult<List<GetPublicTeamResponse>>>
    {
        public int ClientId { get; set; }
    }
}
