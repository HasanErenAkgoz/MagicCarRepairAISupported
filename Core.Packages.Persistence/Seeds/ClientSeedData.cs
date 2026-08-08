using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds;

/// <summary>
/// Minimal platform tenant required for SystemAdmin <see cref="User.ClientId"/> (FK).
/// Additional shops are created manually via the API.
/// </summary>
public static class ClientSeedData
{
    public static List<Client> GetClients()
    {
        var now = DateTime.UtcNow;
        return
        [
            new Client
            {
                Id = 1,
                Name = "Magic Car Repair Platform",
                Code = "PLATFORM",
                Description = "Sistem platform kiracısı — Super Admin için",
                ContactEmail = "admin@magiccar.com",
                IsActive = true,
                IsPublicProfileEnabled = false,
                SubscriptionStartDate = now,
                SubscriptionEndDate = now.AddYears(10),
                CreatedDate = now,
                Status = Status.Active,
            },
        ];
    }
}
