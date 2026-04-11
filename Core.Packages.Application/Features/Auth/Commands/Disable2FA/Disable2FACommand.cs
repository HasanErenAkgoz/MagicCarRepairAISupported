using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.Disable2FA
{
    public class Disable2FACommand : IRequest<IDataResult<Disable2FAResponse>>
    {
        public int UserId { get; set; }
        public string? Password { get; set; } // Optional: Güvenlik için şifre doğrulaması
    }

    public class Disable2FAResponse
    {
        public bool RequiresTwoFactor { get; set; } = false;
    }
}
