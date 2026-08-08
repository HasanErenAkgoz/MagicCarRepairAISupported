namespace MagicCarRepairAISupported.Application.Features.Media;

/// <summary>Parses paths that were persisted by the server's upload service, never request input.</summary>
internal static class StoredMediaPath
{
    internal static bool TryParse(string? value, out string container, out string fileName)
    {
        container = fileName = string.Empty;
        var parts = value?.TrimStart('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts is null || parts.Length < 3 || !string.Equals(parts[0], "uploads", StringComparison.OrdinalIgnoreCase)) return false;
        container = string.Join('/', parts.Skip(1).Take(parts.Length - 2));
        fileName = parts[^1];
        return true;
    }

    internal static string ContentType(string name) => Path.GetExtension(name).ToLowerInvariant() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg", ".png" => "image/png", ".webp" => "image/webp", ".gif" => "image/gif",
        _ => "application/octet-stream"
    };
}
