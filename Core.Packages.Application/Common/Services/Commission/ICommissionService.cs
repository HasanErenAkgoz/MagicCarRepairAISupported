namespace MagicCarRepairAISupported.Application.Common.Services.Commission
{
    /// <summary>
    /// Komisyon servisi
    /// </summary>
    public interface ICommissionService
    {
        /// <summary>
        /// Komisyon hesaplar (%2.5)
        /// </summary>
        Task<decimal> CalculateCommissionAsync(decimal amount, int clientId);

        /// <summary>
        /// Komisyon kaydı oluşturur
        /// </summary>
        Task<int> RecordCommissionAsync(int paymentId, decimal commissionAmount, decimal commissionRate);

        /// <summary>
        /// Toplam komisyonu getirir
        /// </summary>
        Task<decimal> GetTotalCommissionsAsync(int clientId, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Komisyon raporu getirir
        /// </summary>
        Task<CommissionReport> GetCommissionReportAsync(int clientId, DateTime startDate, DateTime endDate);
    }

    public class CommissionReport
    {
        public decimal TotalCommissions { get; set; }
        public decimal PendingCommissions { get; set; }
        public decimal PaidCommissions { get; set; }
        public int TotalTransactions { get; set; }
        public List<CommissionDetail> Details { get; set; } = new List<CommissionDetail>();
    }

    public class CommissionDetail
    {
        public int PaymentId { get; set; }
        public string PaymentNumber { get; set; } = string.Empty;
        public decimal PaymentAmount { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal CommissionRate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
