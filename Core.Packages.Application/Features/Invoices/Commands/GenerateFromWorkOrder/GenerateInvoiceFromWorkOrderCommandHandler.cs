using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.GenerateFromWorkOrder
{
    public class GenerateInvoiceFromWorkOrderCommandHandler : IRequestHandler<GenerateInvoiceFromWorkOrderCommand, GenerateInvoiceFromWorkOrderResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GenerateInvoiceFromWorkOrderCommandHandler(
            IInvoiceRepository invoiceRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GenerateInvoiceFromWorkOrderResponse> Handle(GenerateInvoiceFromWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // WorkOrder'ı getir (Items ve Labors dahil)
            var workOrder = await _workOrderRepository.Query()
                .Include(wo => wo.Items)
                .Include(wo => wo.Labors)
                .FirstOrDefaultAsync(wo => wo.Id == request.WorkOrderId, cancellationToken);

            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { WorkOrderId = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // Toplam tutarı hesapla
            workOrder.CalculateTotal();

            // Invoice oluştur
            var invoice = new Invoice
            {
                InvoiceNumber = Invoice.GenerateInvoiceNumber(),
                InvoiceType = Domain.Enums.InvoiceType.Sales,
                WorkOrderId = workOrder.Id,
                CustomerId = workOrder.CustomerId,
                InvoiceDate = DateTime.UtcNow,
                DueDate = request.DueDate ?? DateTime.UtcNow.AddDays(30), // Varsayılan 30 gün vade
                Description = request.Description ?? $"İş Emri #{workOrder.WorkOrderNumber} faturası",
                Status = Domain.Enums.InvoiceStatus.Pending,
                ClientId = clientId,
                Items = new List<InvoiceItem>()
            };

            // WorkOrderItem'ları InvoiceItem'a çevir
            if (workOrder.Items != null)
            {
                foreach (var workOrderItem in workOrder.Items)
                {
                    var invoiceItem = new InvoiceItem
                    {
                        Description = workOrderItem.Description ?? "Parça",
                        Quantity = workOrderItem.Quantity,
                        UnitPrice = workOrderItem.UnitPrice,
                        TaxRate = workOrderItem.TaxRate,
                        ClientId = clientId
                    };
                    invoiceItem.CalculateTotal();
                    invoice.Items.Add(invoiceItem);
                }
            }

            // WorkOrderLabor'ları InvoiceItem'a çevir
            if (workOrder.Labors != null)
            {
                foreach (var labor in workOrder.Labors)
                {
                    var invoiceItem = new InvoiceItem
                    {
                        Description = labor.OperationName ?? "İşçilik",
                        Quantity = (decimal)labor.DurationHours,
                        UnitPrice = labor.HourlyRate,
                        TaxRate = 20, // Varsayılan %20 KDV
                        ClientId = clientId
                    };
                    invoiceItem.CalculateTotal();
                    invoice.Items.Add(invoiceItem);
                }
            }

            // Toplam tutarı hesapla
            invoice.CalculateTotal();

            // Kaydet
            await _invoiceRepository.AddAsync(invoice, cancellationToken);
            await _invoiceRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<GenerateInvoiceFromWorkOrderResponse>(invoice);
            response.InvoiceId = invoice.Id;
            response.WorkOrderNumber = workOrder.WorkOrderNumber;
            response.StatusName = invoice.Status.ToString();

            return response;
        }
    }
}

