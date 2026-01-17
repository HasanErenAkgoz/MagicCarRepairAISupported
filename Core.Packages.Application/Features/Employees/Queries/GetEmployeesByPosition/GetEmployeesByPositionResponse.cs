using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeesByPosition
{
    public class GetEmployeesByPositionResponse
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public EmployeePosition Position { get; set; }
        public List<string>? Specializations { get; set; }
    }
}

