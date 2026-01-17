using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetOverdue
{
    public class GetOverdueInvoicesQuery : IRequest<List<GetOverdueInvoicesResponse>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}

