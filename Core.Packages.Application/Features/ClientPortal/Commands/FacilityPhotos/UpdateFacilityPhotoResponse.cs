namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.FacilityPhotos
{
    public class UpdateFacilityPhotoResponse
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool IsPublic { get; set; }
        public int DisplayOrder { get; set; }
    }
}
