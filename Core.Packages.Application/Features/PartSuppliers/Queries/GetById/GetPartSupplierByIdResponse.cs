namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetById
{
    public class GetPartSupplierByIdResponse
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? TaxNumber { get; set; }
        public string? TaxOffice { get; set; }
        public string? PaymentTerms { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}

