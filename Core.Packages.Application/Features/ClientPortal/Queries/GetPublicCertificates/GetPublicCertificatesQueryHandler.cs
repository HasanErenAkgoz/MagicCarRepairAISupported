using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicCertificates
{
    public class GetPublicCertificatesQueryHandler : IRequestHandler<GetPublicCertificatesQuery, IDataResult<List<GetPublicCertificatesResponse>>>
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IFileStorageService _fileStorageService;

        public GetPublicCertificatesQueryHandler(
            ICertificateRepository certificateRepository,
            IFileStorageService fileStorageService)
        {
            _certificateRepository = certificateRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<IDataResult<List<GetPublicCertificatesResponse>>> Handle(GetPublicCertificatesQuery request, CancellationToken cancellationToken)
        {
            var certificates = await _certificateRepository.GetActiveCertificatesAsync(request.ClientId, cancellationToken);

            var responses = new List<GetPublicCertificatesResponse>();

            foreach (var certificate in certificates)
            {
                var fileUrl = certificate.CertificateFileUrl ?? string.Empty;
                // File path zaten URL formatında olmalı, direkt kullanabiliriz

                var response = new GetPublicCertificatesResponse
                {
                    Id = certificate.Id,
                    CertificateName = certificate.CertificateName,
                    IssuingOrganization = certificate.IssuingOrganization,
                    CertificateNumber = certificate.CertificateNumber,
                    IssueDate = certificate.IssueDate,
                    ExpiryDate = certificate.ExpiryDate,
                    CertificateFileUrl = fileUrl,
                    Description = certificate.Description,
                    IsValid = certificate.IsValid()
                };

                responses.Add(response);
            }

            return new SuccessDataResult<List<GetPublicCertificatesResponse>>(responses);
        }
    }
}
