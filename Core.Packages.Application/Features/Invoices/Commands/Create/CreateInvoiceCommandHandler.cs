using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.Create
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CreateInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(
            IInvoiceRepository invoiceRepository,
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<Customer, int> customerRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _workOrderRepository = workOrderRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateInvoiceResponse> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder kontrolü (eğer belirtilmişse)
            if (request.WorkOrderId.HasValue)
            {
                var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId.Value);
                if (workOrder == null)
                {
                    throw new DomainException("WORKORDER_NOT_FOUND", new { WorkOrderId = request.WorkOrderId.Value });
                }

                if (workOrder.ClientId != clientId)
                {
                    throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId.Value });
                }
            }

            // Customer kontrolü (Satış faturası için)
            if (request.InvoiceType == Domain.Enums.InvoiceType.Sales && request.CustomerId.HasValue)
            {
                var customer = await _customerRepository.GetByIdAsync(request.CustomerId.Value);
                if (customer == null)
                {
                    throw new DomainException("CUSTOMER_NOT_FOUND", new { CustomerId = request.CustomerId.Value });
                }

                if (customer.ClientId != clientId)
                {
                    throw new DomainException("CUSTOMER_NOT_BELONG_TO_CLIENT", new { CustomerId = request.CustomerId.Value });
                }
            }

            // Invoice oluştur
            var invoice = new Invoice
            {
                InvoiceNumber = Invoice.GenerateInvoiceNumber(),
                InvoiceType = request.InvoiceType,
                WorkOrderId = request.WorkOrderId,
                CustomerId = request.CustomerId,
                SupplierId = request.SupplierId,
                InvoiceDate = request.InvoiceDate,
                DueDate = request.DueDate,
                Description = request.Description,
                Status = Domain.Enums.InvoiceStatus.Pending,
                ClientId = clientId,
                Items = new List<InvoiceItem>()
            };

            // InvoiceItem'ları oluştur
            foreach (var itemDto in request.Items)
            {
                var item = new InvoiceItem
                {
                    InvoiceId = invoice.Id, // Bu geçici, save sonrası güncellenecek
                    Description = itemDto.Description,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    TaxRate = itemDto.TaxRate,
                    ClientId = clientId
                };
                item.CalculateTotal();
                invoice.Items.Add(item);
            }

            // Toplam tutarı hesapla
            invoice.CalculateTotal();

            // Kaydet
            await _invoiceRepository.AddAsync(invoice, cancellationToken);
            await _invoiceRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<CreateInvoiceResponse>(invoice);
            response.InvoiceTypeName = invoice.InvoiceType.ToString();
            response.StatusName = invoice.Status.ToString();
            response.ItemCount = invoice.Items.Count;

            return response;
        }
    }
}

