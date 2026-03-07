namespace MagicCarRepairAISupported.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public int UserType { get; set; }
        public string UserTypeLabel { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
