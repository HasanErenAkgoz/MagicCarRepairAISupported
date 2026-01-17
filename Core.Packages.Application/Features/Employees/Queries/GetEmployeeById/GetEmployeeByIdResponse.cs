using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdResponse
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string? NationalId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public EmployeePosition Position { get; set; }
        public string PositionName { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public string EmploymentStatusName { get; set; }
        public List<string>? Specializations { get; set; }
        public string? Address { get; set; }
        public string? BloodType { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? Notes { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

