namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.Certificates
{
    public class UpdateCertificateResponse
    {
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public string IssuingOrganization { get; set; }
        public string? CertificateNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CertificateFileUrl { get; set; }
        public bool IsPublic { get; set; }
        public int DisplayOrder { get; set; }
    }
}
