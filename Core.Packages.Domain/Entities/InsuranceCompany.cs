using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Sigorta/Kasko firması entity'si
    /// </summary>
    public class InsuranceCompany : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Firma Adı
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Firma Kodu (Benzersiz)
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Yetkili Kişi
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// Telefon
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Adres
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// API Endpoint (Entegrasyon için)
        /// </summary>
        public string? ApiEndpoint { get; set; }

        /// <summary>
        /// API Key/Token (Entegrasyon için - şifrelenmiş saklanmalı)
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// Desteklenen Sigorta Tipleri (JSON formatında)
        /// </summary>
        public string? SupportedInsuranceTypes { get; set; }

        /// <summary>
        /// Aktif/Pasif Durumu
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        // Navigation properties
        public virtual ICollection<InsurancePolicy> Policies { get; set; } = new List<InsurancePolicy>();
    }
}

