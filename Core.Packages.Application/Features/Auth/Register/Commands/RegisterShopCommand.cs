using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Register.Commands
{
    public class RegisterShopCommand : IRequest<IDataResult<RegisterShopResponse>>
    {
        // Shop (Client) bilgileri
        public string ShopName { get; set; } = string.Empty;
        public string ShopCode { get; set; } = string.Empty;
        public string? ShopDescription { get; set; }
        public string ShopContactEmail { get; set; } = string.Empty;
        public string ShopContactPhone { get; set; } = string.Empty;
        public string? ShopAddress { get; set; }

        // Owner (User) bilgileri
        public string OwnerFirstName { get; set; } = string.Empty;
        public string OwnerLastName { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
        public string OwnerPassword { get; set; } = string.Empty;
        public string OwnerConfirmPassword { get; set; } = string.Empty;
        public string? OwnerPhoneNumber { get; set; }
        public string? OwnerIdentityNo { get; set; }
        public string? OwnerAddress { get; set; }
        
        // Tax Office Number
        public string? TaxOfficeNo { get; set; }

        // Shop profile fields
        public string? ShopLogoUrl { get; set; }
        public string? ShopBannerUrl { get; set; }
        public double? ShopLatitude { get; set; }
        public double? ShopLongitude { get; set; }
        
        // Vehicle data (optional - from VehicleInfoScreen)
        public string? VehicleLicensePlate { get; set; }
        public string? VehicleBrand { get; set; }
        public string? VehicleModel { get; set; }
        public int? VehicleYear { get; set; }
        public string? VehicleColor { get; set; }
        public long? VehicleKilometers { get; set; }
    }

    public class RegisterShopResponse
    {
        public int ClientId { get; set; }
        public int OwnerUserId { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
