using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Sigorta/Kasko poliçesi entity'si
    /// </summary>
    public class InsurancePolicy : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Poliçe No
        /// </summary>
        public string PolicyNumber { get; set; }

        /// <summary>
        /// Araç ID
        /// </summary>
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Sigorta Firması ID
        /// </summary>
        public int InsuranceCompanyId { get; set; }
        public virtual InsuranceCompany InsuranceCompany { get; set; }

        /// <summary>
        /// Sigorta Tipi
        /// </summary>
        public InsuranceType InsuranceType { get; set; }

        /// <summary>
        /// Başlangıç Tarihi
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Bitiş Tarihi
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Prim Tutarı
        /// </summary>
        public decimal PremiumAmount { get; set; }

        /// <summary>
        /// Teminat Tutarı
        /// </summary>
        public decimal? CoverageAmount { get; set; }

        /// <summary>
        /// Muafiyet Oranı (0-100)
        /// </summary>
        public decimal DeductiblePercentage { get; set; } = 0;

        /// <summary>
        /// Muafiyet Tutarı
        /// </summary>
        public decimal? DeductibleAmount { get; set; }

        /// <summary>
        /// Durum
        /// </summary>
        public InsuranceStatus Status { get; set; } = InsuranceStatus.Active;

        /// <summary>
        /// Poliçe Dosyası (UploadedFile ID)
        /// </summary>
        public int? PolicyFileId { get; set; }
        public virtual UploadedFile? PolicyFile { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        // Navigation properties
        public virtual ICollection<InsuranceClaim> Claims { get; set; } = new List<InsuranceClaim>();

        /// <summary>
        /// Poliçe geçerli mi kontrolü
        /// </summary>
        public bool IsValid()
        {
            var now = DateTime.UtcNow;
            return Status == InsuranceStatus.Active && 
                   now >= StartDate && 
                   now <= EndDate;
        }

        /// <summary>
        /// Poliçe süresine ne kadar kaldı (gün)
        /// </summary>
        public int DaysUntilExpiration()
        {
            if (Status != InsuranceStatus.Active)
                return -1;

            var days = (EndDate - DateTime.UtcNow).Days;
            return days > 0 ? days : 0;
        }
    }
}

