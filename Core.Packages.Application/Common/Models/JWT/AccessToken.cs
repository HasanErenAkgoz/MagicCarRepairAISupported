using MagicCarRepairAISupported.Application.Common.Models.NewFolder;
using MagicCarRepairAISupported.Application.Features.Auth.Login.Commands;

namespace MagicCarRepairAISupported.Application.Common.Models.JWT
{
    public class AccessToken : IAccessToken
    {
        public List<string> Claims { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public LoginUserInfo User { get; set; }
    }
}