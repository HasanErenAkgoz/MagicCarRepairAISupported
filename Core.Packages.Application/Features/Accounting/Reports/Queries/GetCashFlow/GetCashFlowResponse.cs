namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetCashFlow
{
    public class GetCashFlowResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal OpeningBalance { get; set; } // Başlangıç bakiyesi
        public decimal ClosingBalance { get; set; } // Kapanış bakiyesi
        public decimal TotalInflow { get; set; } // Toplam giriş
        public decimal TotalOutflow { get; set; } // Toplam çıkış
        public decimal NetCashFlow { get; set; } // Net nakit akışı
        public List<CashFlowPeriod> Periods { get; set; } = new();
    }

    public class CashFlowPeriod
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string PeriodLabel { get; set; }
        public decimal Inflow { get; set; }
        public decimal Outflow { get; set; }
        public decimal NetFlow { get; set; }
        public decimal RunningBalance { get; set; }
    }
}
