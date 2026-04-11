using System.Text;

namespace MagicCarRepairAISupported.Application.Common.TextEncoding
{
    /// <summary>
    /// UTF-8 metnin yanlışlıkla ISO-8859-1 (Latin-1) karakter dizisi olarak saklandığı
    /// tipik mojibake durumunu düzeltir (ör. "KÃ¶rÃ¼ÄŸÃ¼" → "Körüğü").
    /// </summary>
    public static class Utf8MojibakeHelper
    {
        private static readonly Encoding Latin1 = Encoding.Latin1;

        /// <summary>
        /// UTF-8 mojibake için tipik başlangıç baytlarının Unicode karşılıkları (Ã, Ä, Â …).
        /// </summary>
        public static bool LooksLikeUtf8Mojibake(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            return value.Contains('\u00C3', StringComparison.Ordinal)
                || value.Contains('\u00C4', StringComparison.Ordinal)
                || value.Contains('\u00C2', StringComparison.Ordinal)
                || value.Contains('\u00C5', StringComparison.Ordinal);
        }

        /// <summary>
        /// Latin-1 olarak kodlanmış UTF-8 baytlarını geri çözer. Uymuyorsa orijinali döner.
        /// </summary>
        public static string? Repair(string? value)
        {
            if (string.IsNullOrEmpty(value) || !LooksLikeUtf8Mojibake(value))
                return value;

            try
            {
                var bytes = Latin1.GetBytes(value);
                var repaired = Encoding.UTF8.GetString(bytes);
                if (repaired.Contains('\uFFFD', StringComparison.Ordinal))
                    return value;
                if (string.Equals(repaired, value, StringComparison.Ordinal))
                    return value;
                return repaired;
            }
            catch (DecoderFallbackException)
            {
                return value;
            }
            catch (ArgumentException)
            {
                return value;
            }
        }

        public static string? RepairOptional(string? value) => Repair(value);
    }
}
