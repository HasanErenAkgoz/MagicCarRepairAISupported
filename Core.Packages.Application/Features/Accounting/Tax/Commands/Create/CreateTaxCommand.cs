using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Create
{
    public class CreateTaxCommand : IRequest<CreateTaxResponse>
    {
        public TaxType TaxType { get; set; }
        public int? Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string? Description { get; set; }
        public string? TaxOffice { get; set; }
        public string? TaxNumber { get; set; }
    }
}
