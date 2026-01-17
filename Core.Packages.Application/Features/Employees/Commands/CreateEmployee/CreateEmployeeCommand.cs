using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeResponse>
    {
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? NationalId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public EmployeePosition Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; } = EmploymentStatus.Active;
        public List<string>? Specializations { get; set; }
        public string? Address { get; set; }
        public string? BloodType { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? Notes { get; set; }
        public int? UserId { get; set; } // Opsiyonel: Eğer bu personelin bir User hesabı varsa
    }
}

