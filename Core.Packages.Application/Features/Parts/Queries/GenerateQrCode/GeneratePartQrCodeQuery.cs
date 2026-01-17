using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GenerateQrCode
{
    public class GeneratePartQrCodeQuery : IRequest<byte[]>
    {
        public int PartId { get; set; }
    }
}
