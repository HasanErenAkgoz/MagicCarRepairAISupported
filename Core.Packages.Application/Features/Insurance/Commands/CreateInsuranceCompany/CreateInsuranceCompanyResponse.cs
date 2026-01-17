namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceCompany
{
    public class CreateInsuranceCompanyResponse
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyCode { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}

