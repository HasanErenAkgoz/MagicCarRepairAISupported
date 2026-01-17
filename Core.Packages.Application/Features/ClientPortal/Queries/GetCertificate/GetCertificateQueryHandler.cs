using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetCertificate
{
    public class GetCertificateQueryHandler : IRequestHandler<GetCertificateQuery, GetCertificateResponse>
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly ITenantService _tenantService;

        public GetCertificateQueryHandler(
            ICertificateRepository certificateRepository,
            ITenantService tenantService)
        {
            _certificateRepository = certificateRepository;
            _tenantService = tenantService;
        }

        public async Task<GetCertificateResponse> Handle(GetCertificateQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var certificate = await _certificateRepository.GetByIdAsync(request.Id);
            if (certificate == null || certificate.ClientId != clientId)
            {
                throw new DomainException("CERTIFICATE_NOT_FOUND");
            }

            return new GetCertificateResponse
            {
                Id = certificate.Id,
                CertificateName = certificate.CertificateName,
                IssuingOrganization = certificate.IssuingOrganization,
                CertificateNumber = certificate.CertificateNumber,
                IssueDate = certificate.IssueDate,
                ExpiryDate = certificate.ExpiryDate,
                CertificateFileUrl = certificate.CertificateFileUrl,
                Description = certificate.Description,
                IsPublic = certificate.IsPublic,
                DisplayOrder = certificate.DisplayOrder,
                IsValid = certificate.IsValid(),
                IsExpired = certificate.IsExpired()
            };
        }
    }
}
