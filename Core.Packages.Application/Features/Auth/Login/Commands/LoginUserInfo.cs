using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Auth.Login.Commands
{
    public class LoginUserInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public int? ClientId { get; set; }
        public bool RequiresTwoFactor { get; set; } = false;
        public bool HasCompletedOnboarding { get; set; } = false;
    }
}
