using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class IncomeSeedData
    {
        public static List<Income> GetIncomes()
        {
            var now = DateTime.UtcNow;
            var today = now.Date;
            var yesterday = today.AddDays(-1);
            
            return new List<Income>
            {
                // Today's Incomes
                new Income
                {
                    Id = 1,
                    WorkOrderId = 6,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 5130m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    TransactionDate = today.AddHours(10),
                    Description = "BMW 3 Series tamir işlemi",
                    InvoiceNumber = "INV-2024-001",
                    CustomerId = 6,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddHours(10),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 2,
                    WorkOrderId = null,
                    IncomeType = IncomeType.PartSale,
                    Amount = 450m,
                    PaymentMethod = PaymentMethod.Cash,
                    TransactionDate = today.AddHours(14),
                    Description = "Parça satışı - Fren balata seti",
                    InvoiceNumber = null,
                    CustomerId = 2,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddHours(14),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 3,
                    WorkOrderId = null,
                    IncomeType = IncomeType.Service,
                    Amount = 320m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    TransactionDate = today.AddHours(16),
                    Description = "Araç yıkama hizmeti",
                    InvoiceNumber = null,
                    CustomerId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddHours(16),
                    CreatedBy = 1
                },
                
                // Yesterday's Incomes
                new Income
                {
                    Id = 4,
                    WorkOrderId = 5,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 2596m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = yesterday.AddHours(11),
                    Description = "Hyundai Elantra bakım işlemi",
                    InvoiceNumber = "INV-2024-002",
                    CustomerId = 5,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = yesterday.AddHours(11),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 5,
                    WorkOrderId = null,
                    IncomeType = IncomeType.ExternalSale,
                    Amount = 1200m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    TransactionDate = yesterday.AddHours(15),
                    Description = "Dış satış - Motor yağı",
                    InvoiceNumber = "INV-2024-003",
                    CustomerId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = yesterday.AddHours(15),
                    CreatedBy = 1
                },
                
                // Last 7 Days Incomes (for weekly chart)
                new Income
                {
                    Id = 6,
                    WorkOrderId = 4,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 1770m,
                    PaymentMethod = PaymentMethod.Cash,
                    TransactionDate = today.AddDays(-6).AddHours(9),
                    Description = "Renault Megane bakım",
                    InvoiceNumber = "INV-2024-004",
                    CustomerId = 4,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-6).AddHours(9),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 7,
                    WorkOrderId = null,
                    IncomeType = IncomeType.PartSale,
                    Amount = 280m,
                    PaymentMethod = PaymentMethod.Cash,
                    TransactionDate = today.AddDays(-5).AddHours(10),
                    Description = "Parça satışı - Hava filtresi",
                    InvoiceNumber = null,
                    CustomerId = 1,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-5).AddHours(10),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 8,
                    WorkOrderId = 3,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 3048m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    TransactionDate = today.AddDays(-4).AddHours(11),
                    Description = "Ford Focus tamir",
                    InvoiceNumber = "INV-2024-005",
                    CustomerId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-4).AddHours(11),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 9,
                    WorkOrderId = null,
                    IncomeType = IncomeType.Service,
                    Amount = 150m,
                    PaymentMethod = PaymentMethod.Cash,
                    TransactionDate = today.AddDays(-3).AddHours(13),
                    Description = "Rot balans hizmeti",
                    InvoiceNumber = null,
                    CustomerId = 2,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-3).AddHours(13),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 10,
                    WorkOrderId = 2,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 2124m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = today.AddDays(-2).AddHours(14),
                    Description = "Volkswagen Golf bakım",
                    InvoiceNumber = "INV-2024-006",
                    CustomerId = 2,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-2).AddHours(14),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 11,
                    WorkOrderId = 1,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 2950m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    TransactionDate = today.AddDays(-1).AddHours(10),
                    Description = "Toyota Corolla tamir",
                    InvoiceNumber = "INV-2024-007",
                    CustomerId = 1,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-1).AddHours(10),
                    CreatedBy = 1
                },
                
                // Older incomes (for monthly stats)
                new Income
                {
                    Id = 12,
                    WorkOrderId = null,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 1800m,
                    PaymentMethod = PaymentMethod.Cash,
                    TransactionDate = now.AddDays(-15),
                    Description = "Eski iş emri geliri",
                    InvoiceNumber = "INV-2024-008",
                    CustomerId = 1,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddDays(-15),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 13,
                    WorkOrderId = null,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 2200m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    TransactionDate = now.AddDays(-20),
                    Description = "Eski iş emri geliri 2",
                    InvoiceNumber = "INV-2024-009",
                    CustomerId = 2,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddDays(-20),
                    CreatedBy = 1
                },
                new Income
                {
                    Id = 14,
                    WorkOrderId = null,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = 3500m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = now.AddDays(-25),
                    Description = "Eski iş emri geliri 3",
                    InvoiceNumber = "INV-2024-010",
                    CustomerId = 3,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = now.AddDays(-25),
                    CreatedBy = 1
                }
            };
        }

        public static void SeedIncomes(this Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            builder.Entity<Income>().HasData(GetIncomes());
        }
    }
}
