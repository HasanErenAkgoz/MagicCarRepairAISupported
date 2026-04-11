namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.AddPhoto
{
    public class AddPartPhotoResponse
    {
        public int PhotoId { get; set; }
        public int PartId { get; set; }
        public string FilePath { get; set; }
        public int DisplayOrder { get; set; }
    }
}
