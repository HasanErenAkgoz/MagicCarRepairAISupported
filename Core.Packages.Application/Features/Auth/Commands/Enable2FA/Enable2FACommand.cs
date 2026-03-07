using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.Enable2FA
{
    public class Enable2FACommand : IRequest<IDataResult<Enable2FAResponse>>
    {
        public int UserId { get; set; }
    }

    public class Enable2FAResponse
    {
        public string Secret { get; set; } = string.Empty;
        public string QrCodeUrl { get; set; } = string.Empty;
        public string ManualEntryKey { get; set; } = string.Empty;
        public List<string> RecoveryCodes { get; set; } = new();
        public string Message { get; set; } = string.Empty;
    }
}
