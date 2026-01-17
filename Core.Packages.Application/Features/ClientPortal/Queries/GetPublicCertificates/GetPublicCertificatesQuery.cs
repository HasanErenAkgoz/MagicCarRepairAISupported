using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicCertificates
{
    public class GetPublicCertificatesQuery : IRequest<IDataResult<List<GetPublicCertificatesResponse>>>
    {
        public int ClientId { get; set; }
    }
}
