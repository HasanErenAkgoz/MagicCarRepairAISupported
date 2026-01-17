using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicProfile
{
    public class GetPublicProfileQuery : IRequest<IDataResult<GetPublicProfileResponse>>
    {
        /// <summary>
        /// Client ID veya Client Code
        /// </summary>
        public string? ClientCode { get; set; }
        public int? ClientId { get; set; }
    }
}
