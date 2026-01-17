namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Create
{
    public class CreatePartSupplierResponse
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}

