using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.Certificates
{
    public class DeleteCertificateCommandHandler : IRequestHandler<DeleteCertificateCommand, DeleteCertificateResponse>
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCertificateCommandHandler(
            ICertificateRepository certificateRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _certificateRepository = certificateRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteCertificateResponse> Handle(DeleteCertificateCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var certificate = await _certificateRepository.GetByIdAsync(request.Id);
            if (certificate == null || certificate.ClientId != clientId)
            {
                throw new DomainException("CERTIFICATE_NOT_FOUND");
            }

            // Soft delete
            _certificateRepository.Delete(certificate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeleteCertificateResponse
            {
                Id = request.Id,
                Success = true,
                Message = "Certificate deleted successfully"
            };
        }
    }
}
