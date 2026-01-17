using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Parça tedarikçisi entity'si
    /// </summary>
    public class PartSupplier : BaseEntity<int>, IClientEntity
    {
        public string CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? TaxNumber { get; set; }
        public string? TaxOffice { get; set; }
        public string? PaymentTerms { get; set; } // Ödeme koşulları (örn: "30 gün vade")
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        // Navigation properties
        public virtual ICollection<Part> Parts { get; set; }
    }
}

