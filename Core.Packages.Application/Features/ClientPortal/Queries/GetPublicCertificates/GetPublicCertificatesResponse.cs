namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicCertificates
{
    public class GetPublicCertificatesResponse
    {
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public string IssuingOrganization { get; set; }
        public string? CertificateNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CertificateFileUrl { get; set; }
        public string? Description { get; set; }
        public bool IsValid { get; set; }
    }
}
