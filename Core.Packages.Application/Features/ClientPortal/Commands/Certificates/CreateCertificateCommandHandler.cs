using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.Certificates
{
    public class CreateCertificateCommandHandler : IRequestHandler<CreateCertificateCommand, CreateCertificateResponse>
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCertificateCommandHandler(
            ICertificateRepository certificateRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _certificateRepository = certificateRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCertificateResponse> Handle(CreateCertificateCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Geçerlilik tarihi kontrolü
            if (request.ExpiryDate.HasValue && request.ExpiryDate.Value < request.IssueDate)
            {
                throw new DomainException("EXPIRY_DATE_MUST_BE_AFTER_ISSUE_DATE");
            }

            var certificate = new Certificate
            {
                CertificateName = request.CertificateName,
                IssuingOrganization = request.IssuingOrganization,
                CertificateNumber = request.CertificateNumber,
                IssueDate = request.IssueDate,
                ExpiryDate = request.ExpiryDate,
                CertificateFileUrl = request.CertificateFileUrl,
                Description = request.Description,
                IsPublic = request.IsPublic,
                DisplayOrder = request.DisplayOrder,
                ClientId = clientId
            };

            await _certificateRepository.AddAsync(certificate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateCertificateResponse
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
