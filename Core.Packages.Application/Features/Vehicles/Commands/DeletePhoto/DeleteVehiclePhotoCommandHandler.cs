using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.DeletePhoto
{
    public class DeleteVehiclePhotoCommandHandler : IRequestHandler<DeleteVehiclePhotoCommand, IResult>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEntityRepository<VehiclePhoto, int> _vehiclePhotoRepository;
        private readonly ITenantService _tenantService;
        private readonly IFileStorageService _fileStorageService;

        public DeleteVehiclePhotoCommandHandler(
            IVehicleRepository vehicleRepository,
            IEntityRepository<VehiclePhoto, int> vehiclePhotoRepository,
            ITenantService tenantService,
            IFileStorageService fileStorageService)
        {
            _vehicleRepository = vehicleRepository;
            _vehiclePhotoRepository = vehiclePhotoRepository;
            _tenantService = tenantService;
            _fileStorageService = fileStorageService;
        }

        public async Task<IResult> Handle(DeleteVehiclePhotoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null)
                throw new DomainException("VEHICLE_NOT_FOUND", new { Id = request.VehicleId });

            if (vehicle.ClientId != clientId)
                throw new DomainException("VEHICLE_NOT_BELONG_TO_CLIENT", new { VehicleId = request.VehicleId });

            var photo = await _vehiclePhotoRepository.GetByIdAsync(request.PhotoId);
            if (photo == null || photo.VehicleId != request.VehicleId)
                return new ErrorResult("Fotoğraf bulunamadı.");

            // Fiziksel dosyayı sil
            if (!string.IsNullOrEmpty(photo.FilePath))
            {
                try
                {
                    // FilePath formatı: /uploads/vehicles/{vehicleId}/{fileName}
                    var pathParts = photo.FilePath.TrimStart('/').Split('/');
                    if (pathParts.Length >= 3)
                    {
                        var containerName = $"{pathParts[1]}/{pathParts[2]}"; // vehicles/{vehicleId}
                        var fileName = pathParts[3]; // Dosya adı
                        await _fileStorageService.DeleteFileAsync(fileName, containerName, cancellationToken);
                    }
                }
                catch
                {
                    // Dosya silme hatası olsa bile devam et
                }
            }

            _vehiclePhotoRepository.Delete(photo);
            await _vehiclePhotoRepository.SaveChangesAsync();

            return new SuccessResult("Fotoğraf başarıyla silindi.");
        }
    }
}
