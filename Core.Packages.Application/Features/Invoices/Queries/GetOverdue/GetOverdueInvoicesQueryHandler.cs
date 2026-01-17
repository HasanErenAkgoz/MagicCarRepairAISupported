using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetOverdue
{
    public class GetOverdueInvoicesQueryHandler : IRequestHandler<GetOverdueInvoicesQuery, List<GetOverdueInvoicesResponse>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetOverdueInvoicesQueryHandler(
            IInvoiceRepository invoiceRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<List<GetOverdueInvoicesResponse>> Handle(GetOverdueInvoicesQuery request, CancellationToken cancellationToken)
        {
            // Vadesi geçen faturaları getir
            var invoices = await _invoiceRepository.GetOverdueInvoicesAsync(cancellationToken);

            // Pagination
            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                invoices = invoices
                    .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .ToList();
            }

            // Customer'ları dahil et
            var invoiceIds = invoices.Select(i => i.Id).ToList();
            var invoicesWithCustomer = await _invoiceRepository.Query()
                .Include(i => i.Customer)
                .Where(i => invoiceIds.Contains(i.Id))
                .ToListAsync(cancellationToken);

            var response = invoicesWithCustomer.Select(invoice => new GetOverdueInvoicesResponse
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerId = invoice.CustomerId,
                CustomerName = invoice.Customer?.FullName,
                InvoiceDate = invoice.InvoiceDate,
                DueDate = invoice.DueDate,
                DaysOverdue = invoice.DueDate.HasValue 
                    ? (DateTime.UtcNow - invoice.DueDate.Value).Days 
                    : 0,
                TotalAmount = invoice.TotalAmount,
                PaidAmount = invoice.PaidAmount,
                RemainingAmount = invoice.TotalAmount - invoice.PaidAmount
            }).ToList();

            return response;
        }
    }
}

