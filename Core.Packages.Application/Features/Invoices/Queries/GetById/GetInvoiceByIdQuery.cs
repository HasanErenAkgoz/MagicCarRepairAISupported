using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetById
{
    public class GetInvoiceByIdQuery : IRequest<GetInvoiceByIdResponse>
    {
        public int Id { get; set; }
    }
}

