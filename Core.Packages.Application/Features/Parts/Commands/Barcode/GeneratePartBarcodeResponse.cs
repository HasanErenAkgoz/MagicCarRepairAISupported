namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.Barcode
{
    public class GeneratePartBarcodeResponse
    {
        public int PartId { get; set; }
        public string PartCode { get; set; }
        public string Barcode { get; set; }
        public string BarcodeType { get; set; }
        public byte[] BarcodeImage { get; set; }
    }
}
