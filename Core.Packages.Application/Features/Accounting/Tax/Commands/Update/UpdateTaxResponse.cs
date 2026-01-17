using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Update
{
    public class UpdateTaxResponse
    {
        public int Id { get; set; }
        public TaxType TaxType { get; set; }
        public string TaxTypeName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public TaxStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
