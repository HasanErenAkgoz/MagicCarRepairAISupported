using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Tamirhanelerin müşterilere verdiği fiyat teklifi
    /// </summary>
    public class QuoteResponse : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Fiyat teklifi isteği ID
        /// </summary>
        public int QuoteRequestId { get; set; }
        public virtual QuoteRequest QuoteRequest { get; set; }

        /// <summary>
        /// Teklif veren tamirhane (Client) ID
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Teklif veren çalışan ID (opsiyonel)
        /// </summary>
        public int? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        /// <summary>
        /// Teklif edilen fiyat
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Teklif açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Teklif geçerlilik süresi (gün)
        /// </summary>
        public int? ValidityDays { get; set; }

        /// <summary>
        /// Tahmini tamamlanma süresi (gün)
        /// </summary>
        public int? EstimatedDays { get; set; }

        /// <summary>
        /// Teklif tutarı
        /// </summary>
        public decimal QuoteAmount { get; set; }

        /// <summary>
        /// İndirim oranı (%)
        /// </summary>
        public decimal? DiscountRate { get; set; }

        /// <summary>
        /// Vergi oranı (%)
        /// </summary>
        public decimal? TaxRate { get; set; }

        /// <summary>
        /// Garanti süresi (ay)
        /// </summary>
        public int? WarrantyMonths { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Teklif geçerlilik tarihi
        /// </summary>
        public DateTime? ValidUntilDate { get; set; }

        /// <summary>
        /// Teklif numarası
        /// </summary>
        public string? QuoteNumber { get; set; }

        /// <summary>
        /// Teklif durumu (Beklemede, Kabul Edildi, Reddedildi)
        /// </summary>
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected

        /// <summary>
        /// Teklif tarihi
        /// </summary>
        public DateTime QuoteDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// İndirim tutarı
        /// </summary>
        public decimal DiscountAmount => QuoteAmount * (DiscountRate ?? 0) / 100;

        /// <summary>
        /// Net tutar (indirim ve vergi sonrası)
        /// </summary>
        public decimal NetAmount
        {
            get
            {
                var discounted = QuoteAmount - DiscountAmount;
                return discounted * (1 + (TaxRate ?? 0) / 100);
            }
        }

        /// <summary>
        /// Teklif geçerli mi?
        /// </summary>
        public bool IsValid()
        {
            return Status == "Pending" &&
                   (!ValidUntilDate.HasValue || ValidUntilDate > DateTime.UtcNow);
        }

        /// <summary>
        /// Teklifi kabul et
        /// </summary>
        public void Accept()
        {
            Status = "Accepted";
        }

        /// <summary>
        /// Teklifi reddet
        /// </summary>
        public void Reject(string? reason = null)
        {
            Status = "Rejected";
        }

        /// <summary>
        /// Vergi ve indirim sonrası toplam tutarı hesaplar
        /// </summary>
        public void CalculateAmounts()
        {
            Amount = NetAmount;
        }

        /// <summary>
        /// Teklif numarası oluşturur
        /// </summary>
        public void GenerateQuoteNumber()
        {
            QuoteNumber = $"QN-{DateTime.UtcNow:yyyyMMdd}-{Id:D5}";
        }
    }
}
