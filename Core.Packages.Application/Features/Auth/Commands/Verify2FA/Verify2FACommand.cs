using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.Verify2FA
{
    public class Verify2FACommand : IRequest<IDataResult<Verify2FAResponse>>
    {
        public int UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool IsSetup { get; set; } = false; // Setup sırasında mı yoksa login sırasında mı?
    }

    public class Verify2FAResponse
    {
        public bool IsValid { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool RequiresTwoFactor { get; set; } // Alias for TwoFactorEnabled (for frontend compatibility)
        public string Message { get; set; } = string.Empty;
    }
}
