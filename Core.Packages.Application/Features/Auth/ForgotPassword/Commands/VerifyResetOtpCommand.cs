using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.ForgotPassword.Commands
{
    public class VerifyResetOtpCommand : IRequest<IDataResult<VerifyResetOtpResponse>>
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
    }

    public class VerifyResetOtpResponse
    {
        public string ResetToken { get; set; } = string.Empty;
        public int ResetTokenExpiresIn { get; set; } // Saniye cinsinden
    }
}
