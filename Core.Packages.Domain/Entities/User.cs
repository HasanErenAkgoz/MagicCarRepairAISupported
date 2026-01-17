using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MagicCarRepairAISupported.Domain.Entities
{
    public partial class User : IdentityUser<int>, IClientEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNo { get; set; }
        public string? Address { get; set; }
        public string Language { get; set; } = "tr";
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public UserType UserType { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
