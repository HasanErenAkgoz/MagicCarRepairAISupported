using MagicCarRepairAISupported.Application.Common.Models.NewFolder;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Common.Services.JWT
{
    public interface ITokenService
    {
        Task<TAccessToken> CreateToken<TAccessToken>(User user, bool rememberMe = false)

          where TAccessToken : IAccessToken, new();

        string GenerateRefreshToken();
        public Task<(string Token, string RefreshToken)> GenerateToken(User user);
    }
}
