using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class EmployeeSeedData
    {
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, EmployeeNo = "EMP001", FirstName = "Ahmet", LastName = "Yılmaz", Email = "ahmet.yilmaz@democlient.com", Phone = "+905321234567", NationalId = "12345678901", Position = EmployeePosition.GeneralManager, Salary = 25000m, HireDate = new DateTime(2020, 1, 15), EmploymentStatus = EmploymentStatus.Active, Address = "İstanbul, Türkiye", BloodType = "A+", EmergencyContact = "Ayşe Yılmaz", EmergencyPhone = "+905329876543", Specializations = JsonSerializer.Serialize(new List<string> { "Genel Yönetim", "Strateji", "İş Geliştirme" }), ClientId = 1, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
                new Employee { Id = 2, EmployeeNo = "EMP002", FirstName = "Mehmet", LastName = "Demir", Email = "mehmet.demir@democlient.com", Phone = "+905331234567", NationalId = "23456789012", Position = EmployeePosition.ServiceManager, Salary = 18000m, HireDate = new DateTime(2020, 3, 1), EmploymentStatus = EmploymentStatus.Active, Address = "İstanbul, Türkiye", BloodType = "0+", EmergencyContact = "Fatma Demir", EmergencyPhone = "+905339876543", Specializations = JsonSerializer.Serialize(new List<string> { "Servis Yönetimi", "Müşteri İlişkileri" }), ClientId = 1, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
                new Employee { Id = 3, EmployeeNo = "EMP003", FirstName = "Ali", LastName = "Kaya", Email = "ali.kaya@democlient.com", Phone = "+905341234567", NationalId = "34567890123", Position = EmployeePosition.MasterMechanic, Salary = 16000m, HireDate = new DateTime(2019, 6, 1), EmploymentStatus = EmploymentStatus.Active, Address = "İstanbul, Türkiye", BloodType = "B+", EmergencyContact = "Zeynep Kaya", EmergencyPhone = "+905349876543", Specializations = JsonSerializer.Serialize(new List<string> { "Motor Bakım", "Elektrik Sistemleri", "Fren Sistemleri", "Ekip Yönetimi" }), ClientId = 1, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
                new Employee { Id = 4, EmployeeNo = "EMP004", FirstName = "Mustafa", LastName = "Çelik", Email = "mustafa.celik@democlient.com", Phone = "+905351234567", NationalId = "45678901234", Position = EmployeePosition.Mechanic, Salary = 13000m, HireDate = new DateTime(2021, 2, 1), EmploymentStatus = EmploymentStatus.Active, Address = "İstanbul, Türkiye", BloodType = "A-", EmergencyContact = "Emine Çelik", EmergencyPhone = "+905359876543", Specializations = JsonSerializer.Serialize(new List<string> { "Motor Bakım", "Şanzıman", "Diferansiyel" }), ClientId = 1, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
                new Employee { Id = 5, EmployeeNo = "EMP005", FirstName = "Hasan", LastName = "Öztürk", Email = "hasan.ozturk@democlient.com", Phone = "+905361234567", NationalId = "56789012345", Position = EmployeePosition.Mechanic, Salary = 12000m, HireDate = new DateTime(2021, 5, 15), EmploymentStatus = EmploymentStatus.Active, Address = "İstanbul, Türkiye", BloodType = "0-", EmergencyContact = "Ayşe Öztürk", EmergencyPhone = "+905369876543", Specializations = JsonSerializer.Serialize(new List<string> { "Fren Sistemleri", "Süspansiyon", "Rot-Balans" }), ClientId = 1, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
                new Employee { Id = 6, EmployeeNo = "EMP006", FirstName = "Emre", LastName = "Yıldız", Email = "emre.yildiz@democlient.com", Phone = "+905371234567", NationalId = "67890123456", Position = EmployeePosition.Electrician, Salary = 14000m, HireDate = new DateTime(2020, 9, 1), EmploymentStatus = EmploymentStatus.Active, Address = "İstanbul, Türkiye", BloodType = "AB+", EmergencyContact = "Seda Yıldız", EmergencyPhone = "+905379876543", Specializations = JsonSerializer.Serialize(new List<string> { "Elektrik Sistemleri", "Elektronik", "Akü ve Jeneratör" }), ClientId = 1, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
                new Employee { Id = 7, EmployeeNo = "EMP007", FirstName = "Caner", LastName = "Arslan", Email = "caner.arslan@democlient.com", Phone = "+905381234567", NationalId = "78901234567", Position = EmployeePosition.BodyworkSpecialist, Salary = 13500m, HireDate = new DateTime(2020, 11, 1), EmploymentStatus = EmploymentStatus.Active, Address = "İstanbul, Türkiye", BloodType = "A+", EmergencyContact = "Elif Arslan", EmergencyPhone = "+905389876543", Specializations = JsonSerializer.Serialize(new List<string> { "Kaporta Tamir", "Kaynak", "Hasar Onarımı" }), ClientId = 1, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
                new Employee { Id = 8, EmployeeNo = "EMP008", FirstName = "Burak", LastName = "Şahin", Email = "burak.sahin@democlient.com", Phone = "+905391234567", NationalId = "89012345678", Position = EmployeePosition.Painter, Salary = 12500m, HireDate = new DateTime(2021, 1, 15), EmploymentStatus = EmploymentStatus.Active, Address = "İstanbul, Türkiye", BloodType = "0+", EmergencyContact = "Merve Şahin", EmergencyPhone = "+905399876543", Specializations = JsonSerializer.Serialize(new List<string> { "Boya", "Vernik", "Renk Eşleme" }), ClientId = 1, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
                new Employee { Id = 9, EmployeeNo = "TST001", FirstName = "Kemal", LastName = "Yalçın", Email = "kemal.yalcin@testclient.com", Phone = "+905401234567", NationalId = "90123456789", Position = EmployeePosition.ServiceManager, Salary = 17000m, HireDate = new DateTime(2021, 8, 1), EmploymentStatus = EmploymentStatus.Active, Address = "Ankara, Türkiye", BloodType = "B+", EmergencyContact = "Sevgi Yalçın", EmergencyPhone = "+905409876543", Specializations = JsonSerializer.Serialize(new List<string> { "Servis Yönetimi", "Kalite Kontrol" }), ClientId = 2, Status = Status.Active, CreatedDate = DateTime.UtcNow, CreatedBy = 1 },
            };
        }

        public static void SeedEmployees(this ModelBuilder builder)
        {
            // Demo Client için örnek çalışanlar
            builder.Entity<Employee>().HasData(
                // Genel Müdür
                new Employee
                {
                    Id = 1,
                    EmployeeNo = "EMP001",
                    FirstName = "Ahmet",
                    LastName = "Yılmaz",
                    Email = "ahmet.yilmaz@democlient.com",
                    Phone = "+905321234567",
                    NationalId = "12345678901",
                    Position = EmployeePosition.GeneralManager,
                    Salary = 25000m,
                    HireDate = new DateTime(2020, 1, 15),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "İstanbul, Türkiye",
                    BloodType = "A+",
                    EmergencyContact = "Ayşe Yılmaz",
                    EmergencyPhone = "+905329876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Genel Yönetim", "Strateji", "İş Geliştirme" }),
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                },

                // Servis Müdürü
                new Employee
                {
                    Id = 2,
                    EmployeeNo = "EMP002",
                    FirstName = "Mehmet",
                    LastName = "Demir",
                    Email = "mehmet.demir@democlient.com",
                    Phone = "+905331234567",
                    NationalId = "23456789012",
                    Position = EmployeePosition.ServiceManager,
                    Salary = 18000m,
                    HireDate = new DateTime(2020, 3, 1),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "İstanbul, Türkiye",
                    BloodType = "0+",
                    EmergencyContact = "Fatma Demir",
                    EmergencyPhone = "+905339876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Servis Yönetimi", "Müşteri İlişkileri" }),
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                },

                // Usta Başı
                new Employee
                {
                    Id = 3,
                    EmployeeNo = "EMP003",
                    FirstName = "Ali",
                    LastName = "Kaya",
                    Email = "ali.kaya@democlient.com",
                    Phone = "+905341234567",
                    NationalId = "34567890123",
                    Position = EmployeePosition.MasterMechanic,
                    Salary = 16000m,
                    HireDate = new DateTime(2019, 6, 1),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "İstanbul, Türkiye",
                    BloodType = "B+",
                    EmergencyContact = "Zeynep Kaya",
                    EmergencyPhone = "+905349876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Motor Bakım", "Elektrik Sistemleri", "Fren Sistemleri", "Ekip Yönetimi" }),
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                },

                // Mekanik 1
                new Employee
                {
                    Id = 4,
                    EmployeeNo = "EMP004",
                    FirstName = "Mustafa",
                    LastName = "Çelik",
                    Email = "mustafa.celik@democlient.com",
                    Phone = "+905351234567",
                    NationalId = "45678901234",
                    Position = EmployeePosition.Mechanic,
                    Salary = 13000m,
                    HireDate = new DateTime(2021, 2, 1),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "İstanbul, Türkiye",
                    BloodType = "A-",
                    EmergencyContact = "Emine Çelik",
                    EmergencyPhone = "+905359876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Motor Bakım", "Şanzıman", "Diferansiyel" }),
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                },

                // Mekanik 2
                new Employee
                {
                    Id = 5,
                    EmployeeNo = "EMP005",
                    FirstName = "Hasan",
                    LastName = "Öztürk",
                    Email = "hasan.ozturk@democlient.com",
                    Phone = "+905361234567",
                    NationalId = "56789012345",
                    Position = EmployeePosition.Mechanic,
                    Salary = 12000m,
                    HireDate = new DateTime(2021, 5, 15),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "İstanbul, Türkiye",
                    BloodType = "0-",
                    EmergencyContact = "Ayşe Öztürk",
                    EmergencyPhone = "+905369876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Fren Sistemleri", "Süspansiyon", "Rot-Balans" }),
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                },

                // Elektrikçi
                new Employee
                {
                    Id = 6,
                    EmployeeNo = "EMP006",
                    FirstName = "Emre",
                    LastName = "Yıldız",
                    Email = "emre.yildiz@democlient.com",
                    Phone = "+905371234567",
                    NationalId = "67890123456",
                    Position = EmployeePosition.Electrician,
                    Salary = 14000m,
                    HireDate = new DateTime(2020, 9, 1),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "İstanbul, Türkiye",
                    BloodType = "AB+",
                    EmergencyContact = "Seda Yıldız",
                    EmergencyPhone = "+905379876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Elektrik Sistemleri", "Elektronik", "Akü ve Jeneratör" }),
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                },

                // Kaporta Ustası
                new Employee
                {
                    Id = 7,
                    EmployeeNo = "EMP007",
                    FirstName = "Caner",
                    LastName = "Arslan",
                    Email = "caner.arslan@democlient.com",
                    Phone = "+905381234567",
                    NationalId = "78901234567",
                    Position = EmployeePosition.BodyworkSpecialist,
                    Salary = 13500m,
                    HireDate = new DateTime(2020, 11, 1),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "İstanbul, Türkiye",
                    BloodType = "A+",
                    EmergencyContact = "Elif Arslan",
                    EmergencyPhone = "+905389876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Kaporta Tamir", "Kaynak", "Hasar Onarımı" }),
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                },

                // Boyacı
                new Employee
                {
                    Id = 8,
                    EmployeeNo = "EMP008",
                    FirstName = "Burak",
                    LastName = "Şahin",
                    Email = "burak.sahin@democlient.com",
                    Phone = "+905391234567",
                    NationalId = "89012345678",
                    Position = EmployeePosition.Painter,
                    Salary = 12500m,
                    HireDate = new DateTime(2021, 1, 15),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "İstanbul, Türkiye",
                    BloodType = "0+",
                    EmergencyContact = "Merve Şahin",
                    EmergencyPhone = "+905399876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Boya", "Vernik", "Renk Eşleme" }),
                    ClientId = 1,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                },

                // Test Client için örnek çalışan
                new Employee
                {
                    Id = 9,
                    EmployeeNo = "TST001",
                    FirstName = "Kemal",
                    LastName = "Yalçın",
                    Email = "kemal.yalcin@testclient.com",
                    Phone = "+905401234567",
                    NationalId = "90123456789",
                    Position = EmployeePosition.ServiceManager,
                    Salary = 17000m,
                    HireDate = new DateTime(2021, 8, 1),
                    EmploymentStatus = EmploymentStatus.Active,
                    Address = "Ankara, Türkiye",
                    BloodType = "B+",
                    EmergencyContact = "Sevgi Yalçın",
                    EmergencyPhone = "+905409876543",
                    Specializations = JsonSerializer.Serialize(new List<string> { "Servis Yönetimi", "Kalite Kontrol" }),
                    ClientId = 2,
                    Status = Status.Active,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = 1
                }
            );
        }
    }
}

