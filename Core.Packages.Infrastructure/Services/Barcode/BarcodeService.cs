using MagicCarRepairAISupported.Application.Common.Services.Barcode;
using ZXing;
using ZXing.Common;

namespace MagicCarRepairAISupported.Infrastructure.Services.Barcode
{
    public class BarcodeService : IBarcodeService
    {
        public byte[] GenerateBarcode(string data, BarcodeType type = BarcodeType.Code128, int width = 300, int height = 100)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException("Barcode data cannot be empty", nameof(data));
            }

            var writer = new BarcodeWriterPixelData
            {
                Format = GetBarcodeFormat(type),
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = 2
                }
            };

            var pixelData = writer.Write(data);

            // Convert pixel data to byte array (PNG format)
            using var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            try
            {
                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            using var stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }

        public bool ValidateBarcode(string barcode, BarcodeType type = BarcodeType.Code128)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return false;
            }

            // Basit format kontrolü
            switch (type)
            {
                case BarcodeType.Code128:
                    // Code128 herhangi bir karakter içerebilir
                    return barcode.Length > 0 && barcode.Length <= 80;
                case BarcodeType.Code39:
                    // Code39 sadece büyük harf, rakam ve bazı özel karakterler içerebilir
                    return System.Text.RegularExpressions.Regex.IsMatch(barcode, @"^[A-Z0-9\-\.\$\/\+\%\s]+$");
                case BarcodeType.EAN13:
                    // EAN13 tam olarak 13 rakam olmalı
                    return System.Text.RegularExpressions.Regex.IsMatch(barcode, @"^\d{13}$");
                default:
                    return false;
            }
        }

        public BarcodeInfo ParseBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return new BarcodeInfo
                {
                    Data = string.Empty,
                    Type = BarcodeType.Code128,
                    IsValid = false
                };
            }

            // Barcode tipini otomatik tespit et
            BarcodeType detectedType = BarcodeType.Code128;
            bool isValid = false;

            // EAN13 kontrolü
            if (System.Text.RegularExpressions.Regex.IsMatch(barcode, @"^\d{13}$"))
            {
                detectedType = BarcodeType.EAN13;
                isValid = true;
            }
            // Code39 kontrolü
            else if (System.Text.RegularExpressions.Regex.IsMatch(barcode, @"^[A-Z0-9\-\.\$\/\+\%\s]+$"))
            {
                detectedType = BarcodeType.Code39;
                isValid = true;
            }
            // Code128 (varsayılan)
            else if (barcode.Length > 0 && barcode.Length <= 80)
            {
                detectedType = BarcodeType.Code128;
                isValid = true;
            }

            return new BarcodeInfo
            {
                Data = barcode,
                Type = detectedType,
                IsValid = isValid
            };
        }

        private BarcodeFormat GetBarcodeFormat(BarcodeType type)
        {
            return type switch
            {
                BarcodeType.Code128 => BarcodeFormat.CODE_128,
                BarcodeType.Code39 => BarcodeFormat.CODE_39,
                BarcodeType.EAN13 => BarcodeFormat.EAN_13,
                _ => BarcodeFormat.CODE_128
            };
        }
    }
}
