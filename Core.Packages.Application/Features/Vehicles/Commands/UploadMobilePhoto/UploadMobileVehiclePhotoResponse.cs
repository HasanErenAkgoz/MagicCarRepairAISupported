namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.UploadMobilePhoto
{
    /// <summary>
    /// Mobil uygulama için araç fotoğrafı yükleme response'u
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class UploadMobileVehiclePhotoResponse
    {
        public PhotoDto Data { get; set; } = new();
    }

    public class PhotoDto
    {
        public string PhotoId { get; set; } = string.Empty;
        /// <summary>Tenant-authorized private-media endpoint.</summary>
        public string MediaUrl { get; set; } = string.Empty;
        /// <summary>Legacy path, retained only for backwards compatibility during migration.</summary>
        public string Url { get; set; } = string.Empty;
    }
}
