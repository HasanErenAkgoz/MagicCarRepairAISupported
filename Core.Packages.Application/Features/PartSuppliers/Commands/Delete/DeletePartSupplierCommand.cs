using MediatR;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Delete
{
    public class DeletePartSupplierCommand : IRequest<DeletePartSupplierResponse>
    {
        public int Id { get; set; }
    }
}

