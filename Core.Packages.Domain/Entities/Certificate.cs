using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Sertifika entity'si - Tamirhane firmasının sertifikaları
    /// </summary>
    public class Certificate : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Sertifika adı
        /// </summary>
        public string CertificateName { get; set; }

        /// <summary>
        /// Sertifika veren kurum/organizasyon
        /// </summary>
        public string IssuingOrganization { get; set; }

        /// <summary>
        /// Sertifika numarası
        /// </summary>
        public string? CertificateNumber { get; set; }

        /// <summary>
        /// Veriliş tarihi
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Geçerlilik tarihi (null ise süresiz)
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Sertifika dosyası/fotoğrafı yolu
        /// </summary>
        public string? CertificateFileUrl { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Public profilde gösterilsin mi?
        /// </summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>
        /// Sıralama
        /// </summary>
        public int DisplayOrder { get; set; } = 0;

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Sertifika geçerli mi kontrol eder
        /// </summary>
        public bool IsValid()
        {
            if (ExpiryDate.HasValue)
            {
                return ExpiryDate.Value >= DateTime.UtcNow;
            }
            return true; // Süresiz sertifika
        }

        /// <summary>
        /// Sertifika süresi dolmuş mu kontrol eder
        /// </summary>
        public bool IsExpired()
        {
            return ExpiryDate.HasValue && ExpiryDate.Value < DateTime.UtcNow;
        }
    }
}
