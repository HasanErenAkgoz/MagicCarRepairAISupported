using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceCompany
{
    public class CreateInsuranceCompanyCommand : IRequest<CreateInsuranceCompanyResponse>
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyCode { get; set; } = string.Empty;
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

