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
