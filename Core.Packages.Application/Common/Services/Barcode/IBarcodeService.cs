namespace MagicCarRepairAISupported.Application.Common.Services.Barcode
{
    /// <summary>
    /// Barcode oluşturma ve okuma servisi
    /// </summary>
    public interface IBarcodeService
    {
        /// <summary>
        /// Barcode oluşturur
        /// </summary>
        /// <param name="data">Barcode içeriği</param>
        /// <param name="type">Barcode tipi (Code128, Code39, EAN13)</param>
        /// <param name="width">Genişlik (piksel)</param>
        /// <param name="height">Yükseklik (piksel)</param>
        /// <returns>Barcode görseli (byte array)</returns>
        byte[] GenerateBarcode(string data, BarcodeType type = BarcodeType.Code128, int width = 300, int height = 100);

        /// <summary>
        /// Barcode formatını doğrular
        /// </summary>
        /// <param name="barcode">Barcode string</param>
        /// <param name="type">Barcode tipi</param>
        /// <returns>Geçerli mi?</returns>
        bool ValidateBarcode(string barcode, BarcodeType type = BarcodeType.Code128);

        /// <summary>
        /// Barcode'dan bilgi çıkarır (parse eder)
        /// </summary>
        /// <param name="barcode">Barcode string</param>
        /// <returns>Parse edilmiş bilgi</returns>
        BarcodeInfo ParseBarcode(string barcode);
    }

    /// <summary>
    /// Barcode tipi
    /// </summary>
    public enum BarcodeType
    {
        Code128,
        Code39,
        EAN13
    }

    /// <summary>
    /// Barcode bilgisi
    /// </summary>
    public class BarcodeInfo
    {
        public string Data { get; set; }
        public BarcodeType Type { get; set; }
        public bool IsValid { get; set; }
    }
}
