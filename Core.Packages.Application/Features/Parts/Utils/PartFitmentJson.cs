using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.Parts.Utils
{
    internal static class PartFitmentJson
    {
        public static string? SerializeStringArray(string[]? values)
        {
            var normalized = Normalize(values);
            if (normalized == null)
            {
                return null;
            }

            return JsonSerializer.Serialize(normalized);
        }

        public static string[]? DeserializeStringArray(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            try
            {
                var parsed = JsonSerializer.Deserialize<string[]>(json);
                return Normalize(parsed);
            }
            catch
            {
                // Invalid JSON in legacy data should not break the API response.
                return null;
            }
        }

        private static string[]? Normalize(string[]? values)
        {
            if (values == null)
            {
                return null;
            }

            var cleaned = values
                .Select(v => (v ?? string.Empty).Trim())
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            return cleaned.Length == 0 ? null : cleaned;
        }
    }
}

