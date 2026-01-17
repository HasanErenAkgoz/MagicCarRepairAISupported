using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetById
{
    public class GetTaxByIdResponse
    {
        public int Id { get; set; }
        public TaxType TaxType { get; set; }
        public string TaxTypeName { get; set; }
        public int? Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public TaxStatus Status { get; set; }
        public string StatusName { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string? PaymentMethodName { get; set; }
        public string? Description { get; set; }
        public string? PaymentReferenceNumber { get; set; }
        public string? TaxOffice { get; set; }
        public string? TaxNumber { get; set; }
        public bool IsOverdue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
