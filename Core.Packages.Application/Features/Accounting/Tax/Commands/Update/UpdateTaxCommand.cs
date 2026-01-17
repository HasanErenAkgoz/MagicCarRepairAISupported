using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Update
{
    public class UpdateTaxCommand : IRequest<UpdateTaxResponse>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string? Description { get; set; }
        public string? TaxOffice { get; set; }
        public string? TaxNumber { get; set; }
    }
}
