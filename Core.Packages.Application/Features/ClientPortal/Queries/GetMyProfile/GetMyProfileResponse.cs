namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetMyProfile
{
    public class GetMyProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string? AboutUs { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LogoUrl { get; set; }
        public string? WorkingHours { get; set; }
        public string? Services { get; set; }
        public string? SocialMediaLinks { get; set; }
        public bool IsPublicProfileEnabled { get; set; }
        public List<FacilityPhotoDto> FacilityPhotos { get; set; } = new();
    }

    public class FacilityPhotoDto
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public string? Title { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
