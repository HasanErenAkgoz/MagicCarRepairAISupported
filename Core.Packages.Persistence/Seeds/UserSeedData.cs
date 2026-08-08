using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MagicCarRepairAISupported.Persistence.Seeds;

public static class UserSeedData
{
    /// <summary>
    /// Tek bootstrap Super Admin. Şifreyi ilk girişten sonra değiştirin.
    /// E-posta: admin@magiccar.com — Şifre: Admin123!
    /// </summary>
    public static List<User> GetUsers()
    {
        var hasher = new PasswordHasher<User>();

        var admin = new User
        {
            FirstName = "Super",
            LastName = "Admin",
            Email = "admin@magiccar.com",
            NormalizedEmail = "ADMIN@MAGICCAR.COM",
            UserName = "admin@magiccar.com",
            NormalizedUserName = "ADMIN@MAGICCAR.COM",
            UserType = UserType.SystemAdmin,
            ClientId = 1,
            Language = "tr",
            HasCompletedOnboarding = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ConcurrencyStamp = Guid.NewGuid().ToString("D"),
            PhoneNumberConfirmed = false,
        };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

        return [admin];
    }
}
