using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceCompany
{
    public class UpdateInsuranceCompanyCommand : IRequest<UpdateInsuranceCompanyResponse>
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        // CompanyCode is immutable - cannot be changed
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ApiEndpoint { get; set; }
        public string? ApiKey { get; set; }
        public string? SupportedInsuranceTypes { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
