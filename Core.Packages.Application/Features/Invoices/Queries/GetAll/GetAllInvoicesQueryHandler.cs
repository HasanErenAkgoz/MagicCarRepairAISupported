using AutoMapper;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetAll
{
    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, List<GetAllInvoicesResponse>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public GetAllInvoicesQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllInvoicesResponse>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Invoice> query = _invoiceRepository.Query()
                .Include(i => i.WorkOrder)
                .Include(i => i.Customer);

            // Filtreler
            if (request.InvoiceType.HasValue)
            {
                query = query.Where(i => i.InvoiceType == request.InvoiceType.Value);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(i => i.Status == request.Status.Value);
            }

            if (request.WorkOrderId.HasValue)
            {
                query = query.Where(i => i.WorkOrderId == request.WorkOrderId.Value);
            }

            if (request.CustomerId.HasValue)
            {
                query = query.Where(i => i.CustomerId == request.CustomerId.Value);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(i => i.InvoiceDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(i => i.InvoiceDate <= request.EndDate.Value);
            }

            // Pagination
            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                             .Take(request.PageSize.Value);
            }

            // Çalıştır ve map et
            var invoices = await query
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<GetAllInvoicesResponse>>(invoices);

            // Enum isimleri ve ilişkili veriler ekle
            foreach (var item in response)
            {
                var invoice = invoices.First(i => i.Id == item.Id);
                item.InvoiceTypeName = invoice.InvoiceType.ToString();
                item.StatusName = invoice.Status.ToString();
                item.WorkOrderNumber = invoice.WorkOrder?.WorkOrderNumber;
                item.CustomerName = invoice.Customer?.FullName;
                item.RemainingAmount = invoice.TotalAmount - invoice.PaidAmount;
            }

            return response;
        }
    }
}

