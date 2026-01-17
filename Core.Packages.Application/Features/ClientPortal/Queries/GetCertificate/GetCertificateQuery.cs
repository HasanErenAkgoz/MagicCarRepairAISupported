using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetCertificate
{
    public class GetCertificateQuery : IRequest<GetCertificateResponse>
    {
        public int Id { get; set; }
    }
}
