using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesResponse
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public EmployeePosition Position { get; set; }
        public string PositionName { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public string EmploymentStatusName { get; set; }
        public DateTime HireDate { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public int? UserId { get; set; }
    }
}

