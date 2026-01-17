using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Delete
{
    public class DeleteTaxCommand : IRequest<DeleteTaxResponse>
    {
        public int Id { get; set; }
    }
}
