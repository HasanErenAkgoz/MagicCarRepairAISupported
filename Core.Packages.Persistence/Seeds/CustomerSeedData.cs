using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class CustomerSeedData
    {
        public static List<Customer> GetCustomers()
        {
            var now = DateTime.UtcNow;
            
            return new List<Customer>
            {
                // Demo Client Customers
                new Customer
                {
                    Id = 1,
                    IdentityNo = "11111111111",
                    FirstName = "Ahmet",
                    LastName = "Yılmaz",
                    Email = "ahmet.yilmaz@example.com",
                    PhoneNumber = "+905321234567",
                    Avatar = "https://i.pravatar.cc/150?u=1",
                    Address = "İstanbul, Kadıköy, Bağdat Caddesi No:123",
                    DateTimeOfBirth = new DateTime(1985, 5, 15),
                    Language = "tr",
                    UserId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-6),
                    CreatedBy = 1
                },
                new Customer
                {
                    Id = 2,
                    IdentityNo = "22222222222",
                    FirstName = "Mehmet",
                    LastName = "Demir",
                    Email = "mehmet.demir@example.com",
                    PhoneNumber = "+905331234568",
                    Avatar = "https://i.pravatar.cc/150?u=2",
                    Address = "İstanbul, Beşiktaş, Barbaros Bulvarı No:45",
                    DateTimeOfBirth = new DateTime(1990, 8, 22),
                    Language = "tr",
                    UserId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-5),
                    CreatedBy = 1
                },
                new Customer
                {
                    Id = 3,
                    IdentityNo = "33333333333",
                    FirstName = "Ayşe",
                    LastName = "Kaya",
                    Email = "ayse.kaya@example.com",
                    PhoneNumber = "+905341234569",
                    Avatar = "https://i.pravatar.cc/150?u=3",
                    Address = "İstanbul, Şişli, Halaskargazi Caddesi No:78",
                    DateTimeOfBirth = new DateTime(1988, 3, 10),
                    Language = "tr",
                    UserId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-4),
                    CreatedBy = 1
                },
                new Customer
                {
                    Id = 4,
                    IdentityNo = "44444444444",
                    FirstName = "Fatma",
                    LastName = "Çelik",
                    Email = "fatma.celik@example.com",
                    PhoneNumber = "+905351234570",
                    Address = "İstanbul, Üsküdar, Bağlarbaşı Caddesi No:12",
                    DateTimeOfBirth = new DateTime(1992, 11, 5),
                    Language = "tr",
                    UserId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-3),
                    CreatedBy = 1
                },
                new Customer
                {
                    Id = 5,
                    IdentityNo = "55555555555",
                    FirstName = "Ali",
                    LastName = "Öztürk",
                    Email = "ali.ozturk@example.com",
                    PhoneNumber = "+905361234571",
                    Address = "İstanbul, Bakırköy, Atatürk Caddesi No:234",
                    DateTimeOfBirth = new DateTime(1987, 7, 18),
                    Language = "tr",
                    UserId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-2),
                    CreatedBy = 1
                },
                new Customer
                {
                    Id = 6,
                    IdentityNo = "66666666666",
                    FirstName = "Zeynep",
                    LastName = "Arslan",
                    Email = "zeynep.arslan@example.com",
                    PhoneNumber = "+905371234572",
                    Address = "İstanbul, Maltepe, Bağdat Caddesi No:567",
                    DateTimeOfBirth = new DateTime(1991, 2, 28),
                    Language = "tr",
                    UserId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-1),
                    CreatedBy = 1
                },
                new Customer
                {
                    Id = 7,
                    IdentityNo = "77777777777",
                    FirstName = "Mustafa",
                    LastName = "Şahin",
                    Email = "mustafa.sahin@example.com",
                    PhoneNumber = "+905381234573",
                    Address = "İstanbul, Ataşehir, Barbaros Mahallesi No:89",
                    DateTimeOfBirth = new DateTime(1989, 9, 12),
                    Language = "tr",
                    UserId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddDays(-15),
                    CreatedBy = 1
                },
                new Customer
                {
                    Id = 8,
                    IdentityNo = "88888888888",
                    FirstName = "Elif",
                    LastName = "Yıldız",
                    Email = "elif.yildiz@example.com",
                    PhoneNumber = "+905391234574",
                    Address = "İstanbul, Pendik, Ertuğrul Gazi Caddesi No:345",
                    DateTimeOfBirth = new DateTime(1993, 4, 25),
                    Language = "tr",
                    UserId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddDays(-7),
                    CreatedBy = 1
                }
            };
        }

        public static void SeedCustomers(this Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            builder.Entity<Customer>().HasData(GetCustomers());
        }
    }
}
