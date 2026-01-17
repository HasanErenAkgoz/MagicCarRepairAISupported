namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetDashboardStats
{
    public class GetDashboardStatsResponse
    {
        // İş Emirleri İstatistikleri
        public int TotalWorkOrders { get; set; }
        public int ActiveWorkOrders { get; set; }
        public int CompletedWorkOrders { get; set; }
        public int PendingWorkOrders { get; set; }

        // Finansal İstatistikler
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetProfit { get; set; }
        public decimal PendingInvoiceAmount { get; set; }
        public decimal OverdueInvoiceAmount { get; set; }

        // Stok İstatistikleri
        public int TotalParts { get; set; }
        public int LowStockParts { get; set; }
        public int OutOfStockParts { get; set; }
        public int ActiveStockAlerts { get; set; }

        // Müşteri İstatistikleri
        public int TotalCustomers { get; set; }
        public int TotalVehicles { get; set; }
        public int NewCustomersThisMonth { get; set; }

        // Teklif İstatistikleri
        public int OpenQuoteRequests { get; set; }
        public int PendingQuoteResponses { get; set; }

        // Bildirim İstatistikleri
        public int UnreadNotifications { get; set; }

        // Tarih Aralığı
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

