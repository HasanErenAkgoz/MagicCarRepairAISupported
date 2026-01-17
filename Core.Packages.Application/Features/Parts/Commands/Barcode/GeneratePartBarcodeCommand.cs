using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.Barcode
{
    public class GeneratePartBarcodeCommand : IRequest<GeneratePartBarcodeResponse>
    {
        public int PartId { get; set; }
        public BarcodeType? BarcodeType { get; set; } // Null ise otomatik seçilir
    }

    public enum BarcodeType
    {
        Code128,
        Code39,
        EAN13
    }
}
