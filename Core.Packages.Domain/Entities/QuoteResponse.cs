using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Teklif yanıtı entity'si - Servislerin verdiği teklifler
    /// </summary>
    public class QuoteResponse : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Teklif talebi ID
        /// </summary>
        public int QuoteRequestId { get; set; }
        public virtual QuoteRequest QuoteRequest { get; set; }

        /// <summary>
        /// Teklif veren servis (Client) ID
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Teklif numarası (Otomatik oluşturulur: QRES-YYYYMMDD-XXXX)
        /// </summary>
        public string QuoteNumber { get; set; }

        /// <summary>
        /// Teklif açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Tahmini süre (Gün)
        /// </summary>
        public int EstimatedDays { get; set; }

        /// <summary>
        /// Teklif tutarı
        /// </summary>
        public decimal QuoteAmount { get; set; }

        /// <summary>
        /// İndirim oranı (%)
        /// </summary>
        public decimal DiscountRate { get; set; } = 0;

        /// <summary>
        /// İndirim tutarı
        /// </summary>
        public decimal DiscountAmount { get; private set; }

        /// <summary>
        /// KDV oranı (%)
        /// </summary>
        public decimal TaxRate { get; set; } = 20; // Varsayılan %20 KDV

        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal TaxAmount { get; private set; }

        /// <summary>
        /// Net tutar (İndirim ve KDV sonrası)
        /// </summary>
        public decimal NetAmount { get; private set; }

        /// <summary>
        /// Garanti süresi (Ay)
        /// </summary>
        public int WarrantyMonths { get; set; } = 0;

        /// <summary>
        /// Durum
        /// </summary>
        public QuoteResponseStatus Status { get; set; } = QuoteResponseStatus.Pending;

        /// <summary>
        /// Teklif tarihi
        /// </summary>
        public DateTime QuoteDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Geçerlilik tarihi (Bu tarihten sonra geçersiz)
        /// </summary>
        public DateTime ValidUntilDate { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Kabul edilme tarihi
        /// </summary>
        public DateTime? AcceptedDate { get; set; }

        /// <summary>
        /// Reddedilme tarihi
        /// </summary>
        public DateTime? RejectedDate { get; set; }

        /// <summary>
        /// Red nedeni
        /// </summary>
        public string? RejectionReason { get; set; }

        /// <summary>
        /// Teklif numarası oluştur
        /// </summary>
        public void GenerateQuoteNumber()
        {
            if (string.IsNullOrEmpty(QuoteNumber))
            {
                var date = DateTime.UtcNow;
                QuoteNumber = $"QRES-{date:yyyyMMdd}-{Id:D4}";
            }
        }

        /// <summary>
        /// Tutarları hesapla
        /// </summary>
        public void CalculateAmounts()
        {
            // İndirim tutarı
            DiscountAmount = QuoteAmount * (DiscountRate / 100);

            // İndirim sonrası tutar
            var amountAfterDiscount = QuoteAmount - DiscountAmount;

            // KDV tutarı
            TaxAmount = amountAfterDiscount * (TaxRate / 100);

            // Net tutar
            NetAmount = amountAfterDiscount + TaxAmount;
        }

        /// <summary>
        /// Teklifi kabul et
        /// </summary>
        public void Accept()
        {
            Status = QuoteResponseStatus.Accepted;
            AcceptedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Teklifi reddet
        /// </summary>
        public void Reject(string? reason = null)
        {
            Status = QuoteResponseStatus.Rejected;
            RejectedDate = DateTime.UtcNow;
            RejectionReason = reason;
        }

        /// <summary>
        /// Teklifi geri çek
        /// </summary>
        public void Withdraw()
        {
            Status = QuoteResponseStatus.Withdrawn;
        }

        /// <summary>
        /// Süresi doldu mu kontrol et
        /// </summary>
        public bool IsExpired()
        {
            return DateTime.UtcNow > ValidUntilDate && Status == QuoteResponseStatus.Pending;
        }

        /// <summary>
        /// Geçerli mi kontrol et
        /// </summary>
        public bool IsValid()
        {
            return Status == QuoteResponseStatus.Pending && !IsExpired();
        }
    }
}

