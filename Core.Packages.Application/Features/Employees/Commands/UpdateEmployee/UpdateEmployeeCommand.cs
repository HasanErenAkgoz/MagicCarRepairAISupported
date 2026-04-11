using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<UpdateEmployeeResponse>
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalId { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public EmployeePosition? Position { get; set; }
        public decimal? Salary { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set; }
        public List<string>? Specializations { get; set; }
        public string? Address { get; set; }
        public string? BloodType { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? Notes { get; set; }
        public string? Biography { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public bool? IsPublic { get; set; }
        public int? DisplayOrder { get; set; }
    }
}

