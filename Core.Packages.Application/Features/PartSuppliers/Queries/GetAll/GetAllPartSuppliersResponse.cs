namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetAll
{
    public class GetAllPartSuppliersResponse
    {
        public List<PartSupplierItem> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class PartSupplierItem
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public bool IsActive { get; set; }
    }
}

