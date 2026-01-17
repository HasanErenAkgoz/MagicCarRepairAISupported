namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetAll
{
    public class GetAllTaxesResponse
    {
        public List<TaxItem> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    public class TaxItem
    {
        public int Id { get; set; }
        public string TaxTypeName { get; set; }
        public int? Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string StatusName { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsOverdue { get; set; }
    }
}
