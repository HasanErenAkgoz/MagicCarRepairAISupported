using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.UploadMobilePhoto
{
    public class UploadMobileVehiclePhotoCommandHandler : IRequestHandler<UploadMobileVehiclePhotoCommand, UploadMobileVehiclePhotoResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEntityRepository<VehiclePhoto, int> _vehiclePhotoRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeRepository _employeeRepository;

        private const long MAX_FILE_SIZE = 10 * 1024 * 1024; // 10 MB
        private const int MAX_PHOTOS_PER_VEHICLE = 10;
        private static readonly string[] ALLOWED_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".webp" };

        public UploadMobileVehiclePhotoCommandHandler(
            IVehicleRepository vehicleRepository,
            IEntityRepository<VehiclePhoto, int> vehiclePhotoRepository,
            IFileStorageService fileStorageService,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeRepository employeeRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehiclePhotoRepository = vehiclePhotoRepository;
            _fileStorageService = fileStorageService;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
            _employeeRepository = employeeRepository;
        }

        public async Task<UploadMobileVehiclePhotoResponse> Handle(UploadMobileVehiclePhotoCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.VehicleId, out var vehicleId))
            {
                throw new DomainException("INVALID_VEHICLE_ID", new { Id = request.VehicleId });
            }

            var clientId = _tenantService.GetRequiredClientId();

            // Vehicle kontrolü
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null || vehicle.ClientId != clientId)
            {
                throw new DomainException("VEHICLE_NOT_FOUND", new { Id = request.VehicleId });
            }

            // Dosya kontrolü
            if (request.Photo == null || request.Photo.Length == 0)
            {
                throw new DomainException("PHOTO_REQUIRED", new { Message = "Fotoğraf dosyası gereklidir." });
            }

            // Dosya boyutu kontrolü
            if (request.Photo.Length > MAX_FILE_SIZE)
            {
                throw new DomainException("FILE_TOO_LARGE", new { Message = $"Dosya boyutu maksimum {MAX_FILE_SIZE / (1024 * 1024)} MB olmalıdır." });
            }

            // Dosya formatı kontrolü
            var fileExtension = Path.GetExtension(request.Photo.FileName).ToLower();
            if (!ALLOWED_EXTENSIONS.Contains(fileExtension))
            {
                throw new DomainException("INVALID_FILE_FORMAT", new { Message = "Sadece JPG, JPEG, PNG ve WEBP formatları desteklenmektedir." });
            }

            // Maksimum fotoğraf sayısı kontrolü
            var existingPhotosCount = await _vehiclePhotoRepository.Query()
                .CountAsync(vp => vp.VehicleId == vehicleId && vp.ClientId == clientId, cancellationToken);

            if (existingPhotosCount >= MAX_PHOTOS_PER_VEHICLE)
            {
                throw new DomainException("MAX_PHOTOS_REACHED", new { Message = $"Araç başına maksimum {MAX_PHOTOS_PER_VEHICLE} fotoğraf yüklenebilir." });
            }

            // Dosya adını UUID ile oluştur
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            
            // Dosyayı yükle
            var containerName = $"vehicles/{vehicleId}";
            
            // IFormFile'ı wrapper ile değiştir (dosya adını UUID yapmak için)
            var wrappedFile = new FormFileWrapper(request.Photo, uniqueFileName);
            var filePath = await _fileStorageService.UploadFileAsync(wrappedFile, containerName, cancellationToken);

            // VehiclePhoto kaydı oluştur
            var userId = GetCurrentUserId();
            var employee = await _employeeRepository.Query()
                .FirstOrDefaultAsync(e => e.UserId == userId && e.ClientId == clientId, cancellationToken);

            var photo = new VehiclePhoto
            {
                VehicleId = vehicleId,
                FilePath = filePath,
                UploadDate = DateTime.UtcNow,
                DisplayOrder = existingPhotosCount, // Son sıraya ekle
                UploadedByEmployeeId = employee?.Id,
                ClientId = clientId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId,
                Status = Domain.Enums.Status.Active
            };

            await _vehiclePhotoRepository.AddAsync(photo, cancellationToken);
            await _vehiclePhotoRepository.SaveChangesAsync();

            return new UploadMobileVehiclePhotoResponse
            {
                Data = new PhotoDto
                {
                    PhotoId = $"ph{photo.Id}",
                    MediaUrl = $"/api/media/vehicles/{vehicleId}/photos/{photo.Id}",
                    Url = filePath.StartsWith("/") ? filePath : $"/{filePath}"
                }
            };
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return 0; // System user
        }
    }
}
