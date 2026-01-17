using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GenerateQrCode
{
    public class GeneratePartQrCodeQueryHandler : IRequestHandler<GeneratePartQrCodeQuery, byte[]>
    {
        private readonly IPartRepository _partRepository;
        private readonly ITenantService _tenantService;
        private readonly IQrCodeService _qrCodeService;

        public GeneratePartQrCodeQueryHandler(
            IPartRepository partRepository,
            ITenantService tenantService,
            IQrCodeService qrCodeService)
        {
            _partRepository = partRepository;
            _tenantService = tenantService;
            _qrCodeService = qrCodeService;
        }

        public async Task<byte[]> Handle(GeneratePartQrCodeQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var part = await _partRepository.GetByIdAsync(request.PartId);
            if (part == null || part.ClientId != clientId)
            {
                throw new DomainException("PART_NOT_FOUND", new { PartId = request.PartId });
            }

            // QR Code içeriği: Part bilgileri
            var qrContent = $"{{\"type\":\"Part\",\"id\":{part.Id},\"code\":\"{part.PartCode}\",\"name\":\"{part.Name}\"}}";

            return _qrCodeService.GenerateQrCode(qrContent, 300);
        }
    }
}
