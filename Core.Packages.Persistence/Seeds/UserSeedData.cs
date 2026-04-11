using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class UserSeedData
    {
        /// <summary>
        /// Returns test users with hashed passwords.
        /// Credentials:
        ///   admin@magiccar.com      / Admin123!     (SystemAdmin, ClientId=1)
        ///   manager@demo.com        / Manager123!   (Manager,     ClientId=1)
        ///   calisan@demo.com        / Calisan123!   (Employee,    ClientId=1)
        ///   musteri@demo.com        / Musteri123!   (Customer,    ClientId=1)
        ///   manager2@test.com       / Manager123!   (Manager,     ClientId=2)
        /// </summary>
        public static List<User> GetUsers()
        {
            var hasher = new PasswordHasher<User>();
            var now = DateTime.UtcNow;

            var admin = new User
            {
                Id = 1,
                FirstName = "Sistem",
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
                PhoneNumber = "+905551234567",
                PhoneNumberConfirmed = true,
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            var manager = new User
            {
                Id = 2,
                FirstName = "Demo",
                LastName = "Yönetici",
                Email = "manager@demo.com",
                NormalizedEmail = "MANAGER@DEMO.COM",
                UserName = "manager@demo.com",
                NormalizedUserName = "MANAGER@DEMO.COM",
                UserType = UserType.Manager,
                ClientId = 1,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905559876543",
                PhoneNumberConfirmed = true,
            };
            manager.PasswordHash = hasher.HashPassword(manager, "Manager123!");

            var employee = new User
            {
                Id = 3,
                FirstName = "Demo",
                LastName = "Çalışan",
                Email = "calisan@demo.com",
                NormalizedEmail = "CALISAN@DEMO.COM",
                UserName = "calisan@demo.com",
                NormalizedUserName = "CALISAN@DEMO.COM",
                UserType = UserType.Employee,
                ClientId = 1,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905551112233",
                PhoneNumberConfirmed = true,
            };
            employee.PasswordHash = hasher.HashPassword(employee, "Calisan123!");

            var customer = new User
            {
                Id = 4,
                FirstName = "Demo",
                LastName = "Müşteri",
                Email = "musteri@demo.com",
                NormalizedEmail = "MUSTERI@DEMO.COM",
                UserName = "musteri@demo.com",
                NormalizedUserName = "MUSTERI@DEMO.COM",
                UserType = UserType.Customer,
                ClientId = 1,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905554445566",
                PhoneNumberConfirmed = true,
            };
            customer.PasswordHash = hasher.HashPassword(customer, "Musteri123!");

            var manager2 = new User
            {
                Id = 5,
                FirstName = "Test",
                LastName = "Yönetici",
                Email = "manager2@test.com",
                NormalizedEmail = "MANAGER2@TEST.COM",
                UserName = "manager2@test.com",
                NormalizedUserName = "MANAGER2@TEST.COM",
                UserType = UserType.Manager,
                ClientId = 2,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905557778899",
                PhoneNumberConfirmed = true,
            };
            manager2.PasswordHash = hasher.HashPassword(manager2, "Manager123!");

            // Yıldız Oto (ClientId=3)
            var managerYildiz = new User
            {
                Id = 6,
                FirstName = "Mehmet",
                LastName = "Yıldız",
                Email = "manager@yildizoto.com.tr",
                NormalizedEmail = "MANAGER@YILDIZOTO.COM.TR",
                UserName = "manager@yildizoto.com.tr",
                NormalizedUserName = "MANAGER@YILDIZOTO.COM.TR",
                UserType = UserType.Manager,
                ClientId = 3,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905321234567",
                PhoneNumberConfirmed = true,
            };
            managerYildiz.PasswordHash = hasher.HashPassword(managerYildiz, "Manager123!");

            // Anadolu Oto Merkezi (ClientId=4)
            var managerAnadolu = new User
            {
                Id = 7,
                FirstName = "Kemal",
                LastName = "Anadolu",
                Email = "manager@anadoluoto.com.tr",
                NormalizedEmail = "MANAGER@ANADOLUOTO.COM.TR",
                UserName = "manager@anadoluoto.com.tr",
                NormalizedUserName = "MANAGER@ANADOLUOTO.COM.TR",
                UserType = UserType.Manager,
                ClientId = 4,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905339876543",
                PhoneNumberConfirmed = true,
            };
            managerAnadolu.PasswordHash = hasher.HashPassword(managerAnadolu, "Manager123!");

            // Boğaz Oto (ClientId=5)
            var managerBogaz = new User
            {
                Id = 8,
                FirstName = "Serkan",
                LastName = "Karadeniz",
                Email = "manager@bogazoto.com.tr",
                NormalizedEmail = "MANAGER@BOGAZOTO.COM.TR",
                UserName = "manager@bogazoto.com.tr",
                NormalizedUserName = "MANAGER@BOGAZOTO.COM.TR",
                UserType = UserType.Manager,
                ClientId = 5,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905344445566",
                PhoneNumberConfirmed = true,
            };
            managerBogaz.PasswordHash = hasher.HashPassword(managerBogaz, "Manager123!");

            // Güneş Oto (ClientId=6)
            var managerGunes = new User
            {
                Id = 9,
                FirstName = "Ahmet",
                LastName = "Güneş",
                Email = "gunesoto@gmail.com",
                NormalizedEmail = "GUNESOTO@GMAIL.COM",
                UserName = "gunesoto@gmail.com",
                NormalizedUserName = "GUNESOTO@GMAIL.COM",
                UserType = UserType.Manager,
                ClientId = 6,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905351112233",
                PhoneNumberConfirmed = true,
            };
            managerGunes.PasswordHash = hasher.HashPassword(managerGunes, "Manager123!");

            // Prestij Auto (ClientId=7)
            var managerPrestij = new User
            {
                Id = 10,
                FirstName = "Emre",
                LastName = "Prestij",
                Email = "info@prestijautomotive.com",
                NormalizedEmail = "INFO@PRESTIJAUTOMOTIVE.COM",
                UserName = "info@prestijautomotive.com",
                NormalizedUserName = "INFO@PRESTIJAUTOMOTIVE.COM",
                UserType = UserType.Manager,
                ClientId = 7,
                Language = "tr",
                HasCompletedOnboarding = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "+905367778899",
                PhoneNumberConfirmed = true,
            };
            managerPrestij.PasswordHash = hasher.HashPassword(managerPrestij, "Manager123!");

            return new List<User>
            {
                admin, manager, employee, customer, manager2,
                managerYildiz, managerAnadolu, managerBogaz, managerGunes, managerPrestij
            };
        }
    }
}
