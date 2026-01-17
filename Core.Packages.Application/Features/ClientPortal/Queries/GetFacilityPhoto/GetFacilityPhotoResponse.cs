namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetFacilityPhoto
{
    public class GetFacilityPhotoResponse
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool IsPublic { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
