using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Create
{
    public class CreateTaxResponse
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
        public string? Description { get; set; }
        public string? TaxOffice { get; set; }
        public string? TaxNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
