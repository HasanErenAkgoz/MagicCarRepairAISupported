using MagicCarRepairAISupported.Application.Common.Services.Import;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Import
{
    public class ImportCustomersResponse
    {
        public int TotalRows { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<ImportError> Errors { get; set; } = new();
    }
}
