using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Fatura entity'si
    /// </summary>
    public class Invoice : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Fatura numarası (Otomatik oluşturulur: INV-YYYYMMDD-XXXX)
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Fatura türü (Satış/Alış)
        /// </summary>
        public InvoiceType InvoiceType { get; set; }

        /// <summary>
        /// İş Emri ID (opsiyonel - satış faturası için)
        /// </summary>
        public int? WorkOrderId { get; set; }
        public virtual WorkOrder? WorkOrder { get; set; }

        /// <summary>
        /// Müşteri ID (Satış faturası için)
        /// </summary>
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Tedarikçi ID (Alış faturası için)
        /// </summary>
        public int? SupplierId { get; set; }
        public virtual PartSupplier? Supplier { get; set; }

        /// <summary>
        /// Fatura tarihi
        /// </summary>
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Vade tarihi
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Ara toplam (KDV hariç)
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Toplam tutar (KDV dahil)
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Ödenen tutar
        /// </summary>
        public decimal PaidAmount { get; set; } = 0;

        /// <summary>
        /// Fatura durumu
        /// </summary>
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// E-Fatura ID (gelecek entegrasyon için)
        /// </summary>
        public string? EInvoiceId { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Fatura kalemleri
        /// </summary>
        public virtual ICollection<InvoiceItem> Items { get; set; }

        /// <summary>
        /// Fatura numarası oluşturur
        /// </summary>
        public static string GenerateInvoiceNumber()
        {
            var prefix = "INV";
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = new Random().Next(1000, 9999);
            return $"{prefix}-{date}-{random}";
        }

        /// <summary>
        /// Toplam tutarı hesaplar
        /// </summary>
        public void CalculateTotal()
        {
            if (Items == null || !Items.Any())
            {
                SubTotal = 0;
                TaxAmount = 0;
                TotalAmount = 0;
                return;
            }

            SubTotal = Items.Sum(i => i.TotalAmount);
            TaxAmount = Items.Sum(i => i.TaxAmount);
            TotalAmount = SubTotal + TaxAmount;
        }

        /// <summary>
        /// Ödeme durumunu günceller
        /// </summary>
        public void UpdatePaymentStatus()
        {
            if (PaidAmount == 0)
            {
                Status = InvoiceStatus.Pending;
            }
            else if (PaidAmount >= TotalAmount)
            {
                Status = InvoiceStatus.Paid;
            }
            else
            {
                Status = InvoiceStatus.PartiallyPaid;
            }

            // Vade kontrolü
            if (DueDate.HasValue && DueDate.Value < DateTime.UtcNow && Status != InvoiceStatus.Paid)
            {
                Status = InvoiceStatus.Overdue;
            }
        }
    }
}

