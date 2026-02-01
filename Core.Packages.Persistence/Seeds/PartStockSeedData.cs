using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class PartStockSeedData
    {
        public static List<PartStock> GetPartStocks()
        {
            var now = DateTime.UtcNow;
            
            return new List<PartStock>
            {
                new PartStock
                {
                    Id = 1,
                    PartId = 1,
                    Quantity = 8,
                    Location = "Raf A-01",
                    LastUpdatedDate = now.AddDays(-5),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-3),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 2,
                    PartId = 2,
                    Quantity = 6,
                    Location = "Raf A-02",
                    LastUpdatedDate = now.AddDays(-3),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-3),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 3,
                    PartId = 3,
                    Quantity = 3, // Low stock (minimum is 10)
                    Location = "Raf B-01",
                    LastUpdatedDate = now.AddDays(-10),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-2),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 4,
                    PartId = 4,
                    Quantity = 18,
                    Location = "Raf B-02",
                    LastUpdatedDate = now.AddDays(-2),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-2),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 5,
                    PartId = 5,
                    Quantity = 25,
                    Location = "Raf C-01",
                    LastUpdatedDate = now.AddDays(-1),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-2),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 6,
                    PartId = 6,
                    Quantity = 1, // Low stock (minimum is 2)
                    Location = "Raf C-02",
                    LastUpdatedDate = now.AddDays(-15),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-1),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 7,
                    PartId = 7,
                    Quantity = 4,
                    Location = "Raf D-01",
                    LastUpdatedDate = now.AddDays(-7),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-1),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 8,
                    PartId = 8,
                    Quantity = 12,
                    Location = "Raf D-02",
                    LastUpdatedDate = now.AddDays(-5),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddMonths(-1),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 9,
                    PartId = 9,
                    Quantity = 6,
                    Location = "Raf E-01",
                    LastUpdatedDate = now.AddDays(-20),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddDays(-30),
                    CreatedBy = 1
                },
                new PartStock
                {
                    Id = 10,
                    PartId = 10,
                    Quantity = 0, // Out of stock (minimum is 4)
                    Location = "Depo-01",
                    LastUpdatedDate = now.AddDays(-25),
                    LastUpdatedByEmployeeId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddDays(-20),
                    CreatedBy = 1
                }
            };
        }

        public static void SeedPartStocks(this Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            builder.Entity<PartStock>().HasData(GetPartStocks());
        }
    }
}
