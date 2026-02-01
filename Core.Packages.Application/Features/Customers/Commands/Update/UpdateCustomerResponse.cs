namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerResponse
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
        public string Language { get; set; }
        public bool IsVip { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

