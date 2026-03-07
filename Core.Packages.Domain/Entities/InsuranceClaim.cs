using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Sigorta hasarı entity'si
    /// </summary>
    public class InsuranceClaim : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Hasar Dosya No
        /// </summary>
        public string ClaimNumber { get; set; }

        /// <summary>
        /// İlgili iş emri ID
        /// </summary>
        public int? WorkOrderId { get; set; }
        public virtual WorkOrder? WorkOrder { get; set; }

        /// <summary>
        /// Sigorta poliçesi ID
        /// </summary>
        public int InsurancePolicyId { get; set; }
        public virtual InsurancePolicy InsurancePolicy { get; set; }

        /// <summary>
        /// Hasar Tarihi
        /// </summary>
        public DateTime DamageDate { get; set; }

        /// <summary>
        /// Hasar Açıklaması
        /// </summary>
        public string DamageDescription { get; set; }

        /// <summary>
        /// Hasar Tutarı
        /// </summary>
        public decimal DamageAmount { get; set; }

        /// <summary>
        /// Onaylanan Tutar
        /// </summary>
        public decimal? ApprovedAmount { get; set; }

        /// <summary>
        /// Muafiyet Tutarı (Hasar Tutarı * Muafiyet Oranı)
        /// </summary>
        public decimal? DeductibleAmount { get; set; }

        /// <summary>
        /// Ödenecek Tutar (Onaylanan Tutar - Muafiyet)
        /// </summary>
        public decimal? PayableAmount { get; set; }

        /// <summary>
        /// Durum
        /// </summary>
        public ClaimStatus Status { get; set; } = ClaimStatus.Applied;

        /// <summary>
        /// Onay Tarihi
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// Ödeme Tarihi
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Red Nedeni
        /// </summary>
        public string? RejectionReason { get; set; }

        /// <summary>
        /// Fotoğraflar (JSON formatında - file paths)
        /// </summary>
        public string? Photos { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Muafiyet tutarını hesapla
        /// </summary>
        public void CalculateDeductible()
        {
            if (InsurancePolicy != null && ApprovedAmount.HasValue)
            {
                if (InsurancePolicy.DeductiblePercentage > 0)
                {
                    DeductibleAmount = ApprovedAmount.Value * (InsurancePolicy.DeductiblePercentage / 100m);
                }
                else if (InsurancePolicy.DeductibleAmount.HasValue)
                {
                    DeductibleAmount = InsurancePolicy.DeductibleAmount.Value;
                }

                PayableAmount = ApprovedAmount.Value - (DeductibleAmount ?? 0);
            }
        }

        /// <summary>
        /// Hasar durumunu onayla
        /// </summary>
        public void Approve(decimal approvedAmount)
        {
            ApprovedAmount = approvedAmount;
            Status = ClaimStatus.Approved;
            ApprovalDate = DateTime.UtcNow;
            CalculateDeductible();
        }

        /// <summary>
        /// Hasar durumunu reddet
        /// </summary>
        public void Reject(string reason)
        {
            Status = ClaimStatus.Rejected;
            RejectionReason = reason;
        }

        /// <summary>
        /// Ödeme yapıldı olarak işaretle
        /// </summary>
        public void MarkAsPaid()
        {
            Status = ClaimStatus.Paid;
            PaymentDate = DateTime.UtcNow;
        }
    }
}

