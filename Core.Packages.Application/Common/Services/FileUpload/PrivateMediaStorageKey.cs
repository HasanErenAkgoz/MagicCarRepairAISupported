namespace MagicCarRepairAISupported.Application.Common.Services.FileUpload;

public static class PrivateMediaStorageKey
{
    public const string Prefix = "private-media/";

    public static bool IsValid(string? storageKey)
    {
        if (string.IsNullOrWhiteSpace(storageKey) || !storageKey.StartsWith(Prefix, StringComparison.Ordinal))
            return false;

        var relative = storageKey[Prefix.Length..];
        if (Path.IsPathRooted(relative)) return false;
        var parts = relative.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
        return parts.Length >= 2 && parts.All(part => part is not "." and not ".." && Path.GetFileName(part) == part);
    }
}
