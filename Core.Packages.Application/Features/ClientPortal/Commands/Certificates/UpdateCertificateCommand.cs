using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.Certificates
{
    public class UpdateCertificateCommand : IRequest<UpdateCertificateResponse>
    {
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public string IssuingOrganization { get; set; }
        public string? CertificateNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CertificateFileUrl { get; set; }
        public string? Description { get; set; }
        public bool IsPublic { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
    }
}
