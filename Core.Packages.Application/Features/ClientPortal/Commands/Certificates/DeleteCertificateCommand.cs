using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.Certificates
{
    public class DeleteCertificateCommand : IRequest<DeleteCertificateResponse>
    {
        public int Id { get; set; }
    }
}
