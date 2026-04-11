using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.ResetPassword
{
    public class ResetPasswordCommand : IRequest<IResult>
    {
        public string ResetToken { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string? ConfirmPassword { get; set; } // Opsiyonel
    }
}
