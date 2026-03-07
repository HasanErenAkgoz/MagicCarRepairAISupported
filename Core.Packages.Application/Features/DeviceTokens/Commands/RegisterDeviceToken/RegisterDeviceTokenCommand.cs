using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.DeviceTokens.Commands.RegisterDeviceToken
{
    public class RegisterDeviceTokenCommand : IRequest<IDataResult<RegisterDeviceTokenResponse>>
    {
        public string Token { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty; // iOS, Android, Web
        public string? DeviceModel { get; set; }
        public string? DeviceOSVersion { get; set; }
    }

    public class RegisterDeviceTokenResponse
    {
        public int DeviceTokenId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
