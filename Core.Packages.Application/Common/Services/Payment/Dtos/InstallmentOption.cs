namespace MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos
{
    /// <summary>
    /// Taksit seçeneği
    /// </summary>
    public class InstallmentOption
    {
        /// <summary>
        /// Taksit sayısı
        /// </summary>
        public int InstallmentCount { get; set; }

        /// <summary>
        /// Aylık ödeme tutarı
        /// </summary>
        public decimal MonthlyAmount { get; set; }

        /// <summary>
        /// Toplam tutar
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Faiz oranı
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// Faizli mi?
        /// </summary>
        public bool HasInterest { get; set; }
    }
}





