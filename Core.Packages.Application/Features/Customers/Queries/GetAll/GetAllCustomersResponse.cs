namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetAll
{
    public class GetAllCustomersResponse
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
        public string Language { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

