using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class ExpenseSeedData
    {
        public static List<Expense> GetExpenses()
        {
            var now = DateTime.UtcNow;
            var today = now.Date;
            var yesterday = today.AddDays(-1);
            
            return new List<Expense>
            {
                // Today's Expenses
                new Expense
                {
                    Id = 1,
                    ExpenseType = ExpenseType.PartPurchase,
                    Amount = 850m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = today.AddHours(9),
                    SupplierName = "Otomotiv Parça A.Ş.",
                    InvoiceNumber = "EXP-2024-001",
                    Description = "Fren balata seti alımı",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddHours(9),
                    CreatedBy = 1
                },
                new Expense
                {
                    Id = 2,
                    ExpenseType = ExpenseType.Electricity,
                    Amount = 1200m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = today.AddHours(10),
                    SupplierName = "TEDAŞ",
                    InvoiceNumber = "ELK-2024-001",
                    Description = "Aylık elektrik faturası",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddHours(10),
                    CreatedBy = 1
                },
                
                // Yesterday's Expenses
                new Expense
                {
                    Id = 3,
                    ExpenseType = ExpenseType.PartPurchase,
                    Amount = 650m,
                    PaymentMethod = PaymentMethod.Cash,
                    TransactionDate = yesterday.AddHours(11),
                    SupplierName = "Yedek Parça Merkezi",
                    InvoiceNumber = "EXP-2024-002",
                    Description = "Motor yağı ve filtre alımı",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = yesterday.AddHours(11),
                    CreatedBy = 1
                },
                new Expense
                {
                    Id = 4,
                    ExpenseType = ExpenseType.Fuel,
                    Amount = 800m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    TransactionDate = yesterday.AddHours(14),
                    SupplierName = "BP Petrol",
                    InvoiceNumber = "FUEL-2024-001",
                    Description = "Araç yakıt gideri",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = yesterday.AddHours(14),
                    CreatedBy = 1
                },
                
                // Last 7 Days Expenses
                new Expense
                {
                    Id = 5,
                    ExpenseType = ExpenseType.Rent,
                    Amount = 5000m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = today.AddDays(-6),
                    SupplierName = "Emlak Sahibi",
                    InvoiceNumber = "RENT-2024-001",
                    Description = "Aylık kira ödemesi",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-6),
                    CreatedBy = 1
                },
                new Expense
                {
                    Id = 6,
                    ExpenseType = ExpenseType.Salary,
                    Amount = 45000m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = today.AddDays(-5),
                    SupplierName = "Personel Maaşları",
                    InvoiceNumber = null,
                    Description = "Aylık personel maaşları",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-5),
                    CreatedBy = 1
                },
                new Expense
                {
                    Id = 7,
                    ExpenseType = ExpenseType.PartPurchase,
                    Amount = 1200m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = today.AddDays(-4),
                    SupplierName = "Otomotiv Parça A.Ş.",
                    InvoiceNumber = "EXP-2024-003",
                    Description = "Süspansiyon parçaları",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-4),
                    CreatedBy = 1
                },
                new Expense
                {
                    Id = 8,
                    ExpenseType = ExpenseType.Water,
                    Amount = 350m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = today.AddDays(-3),
                    SupplierName = "İSKİ",
                    InvoiceNumber = "WTR-2024-001",
                    Description = "Aylık su faturası",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-3),
                    CreatedBy = 1
                },
                new Expense
                {
                    Id = 9,
                    ExpenseType = ExpenseType.PhoneInternet,
                    Amount = 450m,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    TransactionDate = today.AddDays(-2),
                    SupplierName = "Türk Telekom",
                    InvoiceNumber = "TEL-2024-001",
                    Description = "Telefon ve internet faturası",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-2),
                    CreatedBy = 1
                },
                new Expense
                {
                    Id = 10,
                    ExpenseType = ExpenseType.Maintenance,
                    Amount = 600m,
                    PaymentMethod = PaymentMethod.Cash,
                    TransactionDate = today.AddDays(-1),
                    SupplierName = "Teknik Servis",
                    InvoiceNumber = "MNT-2024-001",
                    Description = "Ekipman bakım onarım",
                    EmployeeId = null,
                    PartPurchaseId = null,
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = today.AddDays(-1),
                    CreatedBy = 1
                }
            };
        }

        public static void SeedExpenses(this Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            builder.Entity<Expense>().HasData(GetExpenses());
        }
    }
}
