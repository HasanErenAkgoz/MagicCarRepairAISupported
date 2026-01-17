using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetTaxReport
{
    public class GetTaxReportResponse
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public decimal TotalUnpaidAmount { get; set; }
        public decimal TotalOverdueAmount { get; set; }
        public int TotalTaxCount { get; set; }
        public int PaidTaxCount { get; set; }
        public int UnpaidTaxCount { get; set; }
        public int OverdueTaxCount { get; set; }
        public List<TaxTypeSummary> TaxTypeSummaries { get; set; } = new();
        public List<TaxItem> TaxItems { get; set; } = new();
    }

    public class TaxTypeSummary
    {
        public TaxType TaxType { get; set; }
        public string TaxTypeName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal UnpaidAmount { get; set; }
        public int Count { get; set; }
    }

    public class TaxItem
    {
        public int Id { get; set; }
        public TaxType TaxType { get; set; }
        public string TaxTypeName { get; set; }
        public int? Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public TaxStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? TaxOffice { get; set; }
        public string? TaxNumber { get; set; }
        public bool IsOverdue { get; set; }
        public int DaysUntilDue { get; set; }
    }
}
