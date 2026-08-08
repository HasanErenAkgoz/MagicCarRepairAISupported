using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GenerateQrCode
{
    public class GenerateVehicleQrCodeQueryHandler : IRequestHandler<GenerateVehicleQrCodeQuery, byte[]>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITenantService _tenantService;
        private readonly IQrCodeService _qrCodeService;

        public GenerateVehicleQrCodeQueryHandler(
            IVehicleRepository vehicleRepository,
            ITenantService tenantService,
            IQrCodeService qrCodeService)
        {
            _vehicleRepository = vehicleRepository;
            _tenantService = tenantService;
            _qrCodeService = qrCodeService;
        }

        public async Task<byte[]> Handle(GenerateVehicleQrCodeQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null || vehicle.ClientId != clientId)
            {
                throw new DomainException("VEHICLE_NOT_FOUND", new { VehicleId = request.VehicleId });
            }

            // QR Code içeriği: Vehicle bilgileri
            var qrContent = $"{{\"type\":\"Vehicle\",\"id\":{vehicle.Id},\"licensePlate\":\"{vehicle.LicensePlate}\",\"brand\":\"{vehicle.Brand}\",\"model\":\"{vehicle.Model}\"}}";

            return _qrCodeService.GenerateQrCode(qrContent, 300);
        }
    }
}
