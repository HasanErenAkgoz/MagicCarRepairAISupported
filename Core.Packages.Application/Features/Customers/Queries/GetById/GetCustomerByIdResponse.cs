namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetById
{
    public class GetCustomerByIdResponse
    {
        public int Id { get; set; }
        public string IdentityNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateTimeOfBirth { get; set; }
        public int Age { get; set; }
        public bool IsBirthdayToday { get; set; }
        public string Language { get; set; }
        public bool IsVip { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int VehicleCount { get; set; } // Müşterinin araç sayısı
        
        // Mobil uygulama için eklenen alanlar
        public int ActiveWorkOrders { get; set; }
        public int TotalWorkOrders { get; set; }
        public List<CustomerVehicleSummary> Vehicles { get; set; } = new();
    }

    public class CustomerVehicleSummary
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? FuelType { get; set; }
    }
}

