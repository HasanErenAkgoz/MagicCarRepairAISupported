using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.Certificates
{
    public class UpdateCertificateCommandHandler : IRequestHandler<UpdateCertificateCommand, UpdateCertificateResponse>
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCertificateCommandHandler(
            ICertificateRepository certificateRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _certificateRepository = certificateRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateCertificateResponse> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var certificate = await _certificateRepository.GetByIdAsync(request.Id);
            if (certificate == null || certificate.ClientId != clientId)
            {
                throw new DomainException("CERTIFICATE_NOT_FOUND");
            }

            // Geçerlilik tarihi kontrolü
            if (request.ExpiryDate.HasValue && request.ExpiryDate.Value < request.IssueDate)
            {
                throw new DomainException("EXPIRY_DATE_MUST_BE_AFTER_ISSUE_DATE");
            }

            certificate.CertificateName = request.CertificateName;
            certificate.IssuingOrganization = request.IssuingOrganization;
            certificate.CertificateNumber = request.CertificateNumber;
            certificate.IssueDate = request.IssueDate;
            certificate.ExpiryDate = request.ExpiryDate;
            certificate.CertificateFileUrl = request.CertificateFileUrl;
            certificate.Description = request.Description;
            certificate.IsPublic = request.IsPublic;
            certificate.DisplayOrder = request.DisplayOrder;

            _certificateRepository.Update(certificate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateCertificateResponse
            {
                Id = certificate.Id,
                CertificateName = certificate.CertificateName,
                IssuingOrganization = certificate.IssuingOrganization,
                CertificateNumber = certificate.CertificateNumber,
                IssueDate = certificate.IssueDate,
                ExpiryDate = certificate.ExpiryDate,
                CertificateFileUrl = certificate.CertificateFileUrl,
                IsPublic = certificate.IsPublic,
                DisplayOrder = certificate.DisplayOrder
            };
        }
    }
}
