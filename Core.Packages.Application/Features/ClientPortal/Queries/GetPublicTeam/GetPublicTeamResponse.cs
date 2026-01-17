namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicTeam
{
    public class GetPublicTeamResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string? Biography { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public List<string>? Specializations { get; set; }
        public DateTime HireDate { get; set; }
    }
}
