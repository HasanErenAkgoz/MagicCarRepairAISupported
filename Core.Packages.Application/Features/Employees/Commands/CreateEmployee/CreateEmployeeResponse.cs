using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeResponse
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public EmployeePosition Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

