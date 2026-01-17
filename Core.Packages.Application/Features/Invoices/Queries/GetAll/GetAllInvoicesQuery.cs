using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetAll
{
    public class GetAllInvoicesQuery : IRequest<List<GetAllInvoicesResponse>>
    {
        public InvoiceType? InvoiceType { get; set; }
        public InvoiceStatus? Status { get; set; }
        public int? WorkOrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}

