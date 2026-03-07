using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.AddPhoto
{
    public class AddVehiclePhotoCommandHandler : IRequestHandler<AddVehiclePhotoCommand, AddVehiclePhotoResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEntityRepository<VehiclePhoto, int> _vehiclePhotoRepository;
        private readonly ITenantService _tenantService;

        public AddVehiclePhotoCommandHandler(
            IVehicleRepository vehicleRepository,
            IEntityRepository<VehiclePhoto, int> vehiclePhotoRepository,
            ITenantService tenantService)
        {
            _vehicleRepository = vehicleRepository;
            _vehiclePhotoRepository = vehiclePhotoRepository;
            _tenantService = tenantService;
        }

        public async Task<AddVehiclePhotoResponse> Handle(AddVehiclePhotoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null)
                throw new DomainException("VEHICLE_NOT_FOUND", new { Id = request.VehicleId });

            if (vehicle.ClientId != clientId)
                throw new DomainException("VEHICLE_NOT_BELONG_TO_CLIENT", new { VehicleId = request.VehicleId });

            var photo = new VehiclePhoto
            {
                VehicleId = request.VehicleId,
                FilePath = request.FilePath,
                UploadedFileId = request.UploadedFileId,
                Description = request.Description,
                PhotoType = request.PhotoType,
                DisplayOrder = request.DisplayOrder,
                UploadDate = DateTime.UtcNow,
                ClientId = clientId,
            };

            await _vehiclePhotoRepository.AddAsync(photo, cancellationToken);

            return new AddVehiclePhotoResponse
            {
                PhotoId = photo.Id,
                VehicleId = vehicle.Id,
                FilePath = photo.FilePath,
                PhotoType = photo.PhotoType,
                DisplayOrder = photo.DisplayOrder,
            };
        }
    }
}
