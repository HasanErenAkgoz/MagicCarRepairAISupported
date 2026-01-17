using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.PayTax
{
    public class PayTaxResponse
    {
        public int Id { get; set; }
        public TaxType TaxType { get; set; }
        public string TaxTypeName { get; set; }
        public decimal Amount { get; set; }
        public TaxStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public string? PaymentReferenceNumber { get; set; }
    }
}
