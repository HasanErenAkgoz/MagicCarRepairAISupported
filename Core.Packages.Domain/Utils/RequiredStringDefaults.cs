namespace MagicCarRepairAISupported.Domain.Utils
{
    /// <summary>
    /// Fills missing values for non-nullable string columns before EF persists entities.
    /// Prevents Postgres 23502 when API/tests omit optional-looking fields.
    /// </summary>
    public static class RequiredStringDefaults
    {
        public static string Coalesce(string? value, string fallback = "")
        {
            return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
        }

        public static string ResolveForProperty(string propertyName, string? value, int? maxLength = null)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var trimmed = value.Trim();
                return maxLength is > 0 && trimmed.Length > maxLength
                    ? trimmed[..maxLength.Value]
                    : trimmed;
            }

            var resolved = propertyName switch
            {
                "IdentityNo" or "NationalId" => GenerateNumericPlaceholder(11),
                "PartCode" => GenerateCode("PRT"),
                "LicensePlate" => GenerateCode("PLT"),
                "EmployeeNo" => GenerateCode("EMP"),
                "InvoiceNumber" => GenerateCode("INV"),
                var name when name.EndsWith("Code", StringComparison.Ordinal) => GenerateCode("GEN"),
                _ => string.Empty
            };

            return maxLength is > 0 && resolved.Length > maxLength
                ? resolved[..maxLength.Value]
                : resolved;
        }

        public static string ResolveIdentityNo(string? value) =>
            ResolveForProperty("IdentityNo", value, 11);

        public static string ResolveCode(string? value, string prefix, int? maxLength = 50) =>
            string.IsNullOrWhiteSpace(value)
                ? Truncate(GenerateCode(prefix), maxLength)
                : Truncate(value.Trim(), maxLength);

        private static string GenerateCode(string prefix)
        {
            return $"{prefix}-{Guid.NewGuid():N}".ToUpperInvariant();
        }

        private static string GenerateNumericPlaceholder(int length)
        {
            if (length < 2)
                return "0";

            var randomDigits = Random.Shared.NextInt64(0, (long)Math.Pow(10, length - 2));
            return $"99{randomDigits.ToString().PadLeft(length - 2, '0')}";
        }

        private static string Truncate(string value, int? maxLength)
        {
            if (maxLength is not > 0 || value.Length <= maxLength)
                return value;

            return value[..maxLength.Value];
        }
    }
}
