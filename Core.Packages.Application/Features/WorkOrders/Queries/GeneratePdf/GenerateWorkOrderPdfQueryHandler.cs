using MagicCarRepairAISupported.Application.Common.Services.WorkOrder;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GeneratePdf
{
    public class GenerateWorkOrderPdfQueryHandler : IRequestHandler<GenerateWorkOrderPdfQuery, byte[]>
    {
        private readonly IPdfWorkOrderService _pdfWorkOrderService;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GenerateWorkOrderPdfQueryHandler(
            IPdfWorkOrderService pdfWorkOrderService,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _pdfWorkOrderService = pdfWorkOrderService;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<byte[]> Handle(GenerateWorkOrderPdfQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { WorkOrderId = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            return await _pdfWorkOrderService.GenerateWorkOrderPdfAsync(request.WorkOrderId, cancellationToken);
        }
    }
}
