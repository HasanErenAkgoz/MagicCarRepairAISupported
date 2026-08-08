using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.BulkDeleteParts
{
    public class BulkDeletePartsCommand : IRequest<BulkDeletePartsResponse>
    {
        public List<int> Ids { get; set; } = new();
    }
}

