using MediatR;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetById
{
    public class GetPartSupplierByIdQuery : IRequest<GetPartSupplierByIdResponse>
    {
        public int Id { get; set; }
    }
}

