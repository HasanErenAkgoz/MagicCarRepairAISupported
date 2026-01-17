namespace MagicCarRepairAISupported.Application.Features.Payments.Queries.GetInstallmentOptions
{
    /// <summary>
    /// Taksit seçeneği yanıtı
    /// </summary>
    public class GetInstallmentOptionsResponse
    {
        public int InstallmentCount { get; set; }
        public decimal MonthlyAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public bool HasInterest { get; set; }
    }
}





