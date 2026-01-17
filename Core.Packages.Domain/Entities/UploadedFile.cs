using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Domain.Entities
{
    public class UploadedFile : BaseEntity<int>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public FileType FileType { get; set; }

    }
}
