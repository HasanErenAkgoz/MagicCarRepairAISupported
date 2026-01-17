using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicFacilities
{
    public class GetPublicFacilitiesQuery : IRequest<IDataResult<List<GetPublicFacilitiesResponse>>>
    {
        public int ClientId { get; set; }
        public string? Category { get; set; }
    }
}
