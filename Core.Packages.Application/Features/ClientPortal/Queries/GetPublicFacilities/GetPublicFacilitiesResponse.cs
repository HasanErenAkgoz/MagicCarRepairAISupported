namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicFacilities
{
    public class GetPublicFacilitiesResponse
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
