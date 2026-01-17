using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetById
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, GetInvoiceByIdResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetInvoiceByIdQueryHandler(
            IInvoiceRepository invoiceRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetInvoiceByIdResponse> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Faturayı bul (Items, WorkOrder, Customer, Supplier dahil)
            var invoice = await _invoiceRepository.Query()
                .Include(i => i.Items)
                .Include(i => i.WorkOrder)
                .Include(i => i.Customer)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (invoice == null)
            {
                throw new DomainException("INVOICE_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (invoice.ClientId != clientId)
            {
                throw new DomainException("INVOICE_NOT_BELONG_TO_CLIENT", new { InvoiceId = request.Id });
            }

            // Response
            var response = _mapper.Map<GetInvoiceByIdResponse>(invoice);
            response.InvoiceTypeName = invoice.InvoiceType.ToString();
            response.StatusName = invoice.Status.ToString();
            response.WorkOrderNumber = invoice.WorkOrder?.WorkOrderNumber;
            response.CustomerName = invoice.Customer?.FullName;
            response.SupplierName = invoice.Supplier?.CompanyName;
            response.RemainingAmount = invoice.TotalAmount - invoice.PaidAmount;

            // Items'ı map et
            if (invoice.Items != null)
            {
                response.Items = invoice.Items.Select(item => new InvoiceItemDto
                {
                    Id = item.Id,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TaxRate = item.TaxRate,
                    TaxAmount = item.TaxAmount,
                    TotalAmount = item.TotalAmount
                }).ToList();
            }

            return response;
        }
    }
}

