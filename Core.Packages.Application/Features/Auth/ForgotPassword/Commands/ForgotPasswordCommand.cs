using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.ForgotPassword.Commands
{
    public class ForgotPasswordCommand : IRequest<IDataResult<ForgotPasswordResponse>>
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class ForgotPasswordResponse
    {
        public int OtpExpiresIn { get; set; } // Saniye cinsinden
        public string MaskedPhone { get; set; } = string.Empty;
    }
}
