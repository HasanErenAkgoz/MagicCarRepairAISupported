using MediatR;
using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.UploadMobilePhoto
{
    /// <summary>
    /// Mobil uygulama için araç fotoğrafı yükleme command'ı
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class UploadMobileVehiclePhotoCommand : IRequest<UploadMobileVehiclePhotoResponse>
    {
        public string VehicleId { get; set; } = string.Empty;
        public IFormFile Photo { get; set; } = null!;
    }
}
