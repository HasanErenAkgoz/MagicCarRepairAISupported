using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Personel maaş ödemeleri entity'si
    /// </summary>
    public class SalaryPayment : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Personel ID
        /// </summary>
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// Dönem (Ay)
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Dönem (Yıl)
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Brüt Maaş
        /// </summary>
        public decimal GrossSalary { get; set; }

        /// <summary>
        /// SGK Kesintisi (İşçi Payı)
        /// </summary>
        public decimal SocialSecurityDeduction { get; set; }

        /// <summary>
        /// İşsizlik Sigortası Kesintisi (İşçi Payı)
        /// </summary>
        public decimal UnemploymentInsuranceDeduction { get; set; }

        /// <summary>
        /// Gelir Vergisi Kesintisi
        /// </summary>
        public decimal IncomeTaxDeduction { get; set; }

        /// <summary>
        /// Damga Vergisi
        /// </summary>
        public decimal StampTax { get; set; }

        /// <summary>
        /// Diğer Kesintiler
        /// </summary>
        public decimal OtherDeductions { get; set; }

        /// <summary>
        /// Net Maaş (Brüt - Tüm Kesintiler)
        /// </summary>
        public decimal NetSalary { get; set; }

        /// <summary>
        /// Ödeme Tarihi
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Ödeme Yöntemi
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Ödeme Referans No (Banka işlem no, vb.)
        /// </summary>
        public string? PaymentReferenceNumber { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Net maaşı hesapla
        /// </summary>
        public void CalculateNetSalary()
        {
            NetSalary = GrossSalary - SocialSecurityDeduction - UnemploymentInsuranceDeduction 
                      - IncomeTaxDeduction - StampTax - OtherDeductions;
        }
    }
}
