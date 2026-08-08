using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GenerateQrCode
{
    public class GenerateWorkOrderQrCodeQueryHandler : IRequestHandler<GenerateWorkOrderQrCodeQuery, byte[]>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IQrCodeService _qrCodeService;

        public GenerateWorkOrderQrCodeQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IQrCodeService qrCodeService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _qrCodeService = qrCodeService;
        }

        public async Task<byte[]> Handle(GenerateWorkOrderQrCodeQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null || workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { WorkOrderId = request.WorkOrderId });
            }

            // QR Code içeriği: WorkOrder detay sayfası URL'i veya JSON data
            var qrContent = $"{{\"type\":\"WorkOrder\",\"id\":{workOrder.Id},\"number\":\"{workOrder.WorkOrderNumber}\"}}";

            return _qrCodeService.GenerateQrCode(qrContent, 300);
        }
    }
}
