using System;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class VehicleSeedData
    {
        public static List<Vehicle> GetVehicles()
        {
            return new List<Vehicle>
            {
                // Customer 1 Vehicles
                new Vehicle
                {
                    Id = 1,
                    CustomerId = 1,
                    LicensePlate = "34ABC123",
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2020,
                    Color = "Beyaz",
                    Vin = "JT2BF28K504123456",
                    ModelVariant = "XLI",
                    Trim = "Comfort",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-6),
                    CreatedBy = 1
                },
                new Vehicle
                {
                    Id = 2,
                    CustomerId = 1,
                    LicensePlate = "34DEF456",
                    Brand = "Honda",
                    Model = "Civic",
                    Year = 2019,
                    Color = "Siyah",
                    Vin = "19XFC2F59KE123456",
                    ModelVariant = "Sedan",
                    Trim = "EX",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-5),
                    CreatedBy = 1
                },
                // Customer 2 Vehicles
                new Vehicle
                {
                    Id = 3,
                    CustomerId = 2,
                    LicensePlate = "34GHI789",
                    Brand = "Volkswagen",
                    Model = "Golf",
                    Year = 2021,
                    Color = "Gri",
                    Vin = "WVWZZZ1KZBW123456",
                    ModelVariant = "Hatchback",
                    Trim = "Highline",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-5),
                    CreatedBy = 1
                },
                // Customer 3 Vehicles
                new Vehicle
                {
                    Id = 4,
                    CustomerId = 3,
                    LicensePlate = "34JKL012",
                    Brand = "Ford",
                    Model = "Focus",
                    Year = 2018,
                    Color = "Kırmızı",
                    Vin = "WF0AXXWPW8H123456",
                    ModelVariant = "Hatchback",
                    Trim = "Titanium",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-4),
                    CreatedBy = 1
                },
                // Customer 4 Vehicles
                new Vehicle
                {
                    Id = 5,
                    CustomerId = 4,
                    LicensePlate = "34MNO345",
                    Brand = "Renault",
                    Model = "Megane",
                    Year = 2020,
                    Color = "Mavi",
                    Vin = "VF1RZ0H0Y12345678",
                    ModelVariant = "Sedan",
                    Trim = "Zen",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-3),
                    CreatedBy = 1
                },
                // Customer 5 Vehicles
                new Vehicle
                {
                    Id = 6,
                    CustomerId = 5,
                    LicensePlate = "34PQR678",
                    Brand = "Hyundai",
                    Model = "Elantra",
                    Year = 2019,
                    Color = "Beyaz",
                    Vin = "KMHDN45D5KU123456",
                    ModelVariant = "Sedan",
                    Trim = "Premium",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-2),
                    CreatedBy = 1
                },
                // Customer 6 Vehicles
                new Vehicle
                {
                    Id = 7,
                    CustomerId = 6,
                    LicensePlate = "34STU901",
                    Brand = "BMW",
                    Model = "3 Series",
                    Year = 2021,
                    Color = "Siyah",
                    Vin = "WBA3A5C59EK123456",
                    ModelVariant = "Sedan",
                    Trim = "320i",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-1),
                    CreatedBy = 1
                },
                // Customer 7 Vehicles
                new Vehicle
                {
                    Id = 8,
                    CustomerId = 7,
                    LicensePlate = "34VWX234",
                    Brand = "Mercedes-Benz",
                    Model = "C-Class",
                    Year = 2020,
                    Color = "Gümüş",
                    Vin = "WDDWF4KB5LR123456",
                    ModelVariant = "Sedan",
                    Trim = "C200",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddDays(-15),
                    CreatedBy = 1
                },
                // Customer 8 Vehicles
                new Vehicle
                {
                    Id = 9,
                    CustomerId = 8,
                    LicensePlate = "34YZA567",
                    Brand = "Audi",
                    Model = "A4",
                    Year = 2019,
                    Color = "Beyaz",
                    Vin = "WAUZZZ8K9KA123456",
                    ModelVariant = "Sedan",
                    Trim = "Premium",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddDays(-7),
                    CreatedBy = 1
                },
                // Additional vehicles for more work orders
                new Vehicle
                {
                    Id = 10,
                    CustomerId = 2,
                    LicensePlate = "34BCD890",
                    Brand = "Opel",
                    Model = "Astra",
                    Year = 2017,
                    Color = "Gri",
                    Vin = "W0L0ZCF5812345678",
                    ModelVariant = "Hatchback",
                    Trim = "Elegance",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-4),
                    CreatedBy = 1
                },
                new Vehicle
                {
                    Id = 11,
                    CustomerId = 3,
                    LicensePlate = "34EFG123",
                    Brand = "Peugeot",
                    Model = "308",
                    Year = 2018,
                    Color = "Beyaz",
                    Vin = "VF3XXXXXXXXX123456",
                    ModelVariant = "Hatchback",
                    Trim = "Allure",
                    Status = VehicleStatus.Registered,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow.AddMonths(-3),
                    CreatedBy = 1
                }
            };
        }

        // Vehicle ID -> Kilometers mapping for seed data
        public static Dictionary<int, long> GetVehicleKilometers()
        {
            return new Dictionary<int, long>
            {
                { 1, 45000 },
                { 2, 52000 },
                { 3, 28000 },
                { 4, 67000 },
                { 5, 38000 },
                { 6, 55000 },
                { 7, 25000 },
                { 8, 42000 },
                { 9, 48000 },
                { 10, 89000 },
                { 11, 72000 }
            };
        }

        public static void SeedVehicles(this Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            builder.Entity<Vehicle>().HasData(GetVehicles());
        }
    }
}
