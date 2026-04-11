using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.ForgotPassword.Commands
{
    public class ResendResetOtpCommand : IRequest<IDataResult<ForgotPasswordResponse>>
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
