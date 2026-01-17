using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.Barcode
{
    public class GetPartByBarcodeQuery : IRequest<GetPartByBarcodeResponse>
    {
        public string Barcode { get; set; }
    }
}
