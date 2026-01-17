using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Delete
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, DeleteVehicleResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITenantService _tenantService;

        public DeleteVehicleCommandHandler(
            IVehicleRepository vehicleRepository,
            ITenantService tenantService)
        {
            _vehicleRepository = vehicleRepository;
            _tenantService = tenantService;
        }

        public async Task<DeleteVehicleResponse> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Aracı bul
            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id);
            if (vehicle == null)
            {
                throw new DomainException("VEHICLE_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (vehicle.ClientId != clientId)
            {
                throw new DomainException("VEHICLE_NOT_BELONG_TO_CLIENT", new { VehicleId = request.Id });
            }

            // Soft delete (Status = Cancelled)
            vehicle.Status = VehicleStatus.Cancelled;
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveChangesAsync();

            return new DeleteVehicleResponse
            {
                Id = request.Id,
                Success = true,
                Message = "Vehicle deleted successfully"
            };
        }
    }
}

