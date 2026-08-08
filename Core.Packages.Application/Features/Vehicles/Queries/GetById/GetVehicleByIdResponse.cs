using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetById
{
    public class GetVehicleByIdResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string? FuelType { get; set; }
        public long Kilometers { get; set; }
        public VehicleStatus Status { get; set; }
        public string StatusName { get; set; }
        public VehicleType VehicleType { get; set; }
        public string VehicleTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Araç fotoğrafları (DB'den Include edilerek gelir)
        /// </summary>
        public List<VehiclePhotoDto> Photos { get; set; } = new();

        /// <summary>
        /// Sigorta/Kasko poliçeleri
        /// </summary>
        public List<InsurancePolicyDto> InsurancePolicies { get; set; } = new();
    }

    public class InsurancePolicyDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public string InsuranceCompanyName { get; set; } = string.Empty;
        public InsuranceType InsuranceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public InsuranceStatus Status { get; set; }
        public int DaysUntilExpiration { get; set; }
    }

    public class VehiclePhotoDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Legacy storage path. Kept during the mobile migration; do not use this to request media.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Tenant-authorized URL for this private vehicle photo.
        /// </summary>
        public string MediaUrl { get; set; } = string.Empty;
        public string? PhotoType { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
