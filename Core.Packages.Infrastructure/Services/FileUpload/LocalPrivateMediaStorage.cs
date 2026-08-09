using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MagicCarRepairAISupported.Infrastructure.Services.FileUpload;

/// <summary>
/// Local private media store. Its root is deliberately outside wwwroot, so a
/// storage key cannot become an anonymous static-file URL.
/// </summary>
public sealed class LocalPrivateMediaStorage : IPrivateMediaStorage
{
    private readonly string _rootPath;

    public LocalPrivateMediaStorage(IWebHostEnvironment environment, IConfiguration configuration)
    {
        var configuredRoot = configuration["PrivateMedia:RootPath"];
        _rootPath = string.IsNullOrWhiteSpace(configuredRoot)
            ? Path.Combine(environment.ContentRootPath, "App_Data", "private-media")
            : Path.GetFullPath(configuredRoot);
        _rootPath = Path.GetFullPath(_rootPath);

        var webRoot = Path.GetFullPath(environment.WebRootPath);
        var webRootWithSeparator = webRoot.EndsWith(Path.DirectorySeparatorChar) ? webRoot : webRoot + Path.DirectorySeparatorChar;
        if (_rootPath.StartsWith(webRootWithSeparator, StringComparison.OrdinalIgnoreCase) || string.Equals(_rootPath, webRoot, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Private media storage must be outside the web root.");

        Directory.CreateDirectory(_rootPath);
    }

    public async Task<string> StoreAsync(IFormFile file, string scope, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(file);
        var safeScope = NormalizeScope(scope);
        var extension = GetSafeExtension(file.FileName);
        var fileName = $"{Guid.NewGuid():N}{extension}";
        var key = $"{PrivateMediaStorageKey.Prefix}{safeScope}/{fileName}";
        var path = ResolvePath(key);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        await using var destination = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, useAsync: true);
        await file.CopyToAsync(destination, cancellationToken);
        return key;
    }

    public Task<Stream?> OpenReadAsync(string storageKey, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var path = ResolvePath(storageKey);
        Stream? stream = File.Exists(path)
            ? new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, useAsync: true)
            : null;
        return Task.FromResult(stream);
    }

    public Task<bool> DeleteAsync(string storageKey, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var path = ResolvePath(storageKey);
        if (!File.Exists(path)) return Task.FromResult(false);
        File.Delete(path);
        return Task.FromResult(true);
    }

    private string ResolvePath(string storageKey)
    {
        if (!PrivateMediaStorageKey.IsValid(storageKey))
            throw new ArgumentException("Invalid private media storage key.", nameof(storageKey));

        var relative = storageKey[PrivateMediaStorageKey.Prefix.Length..];
        var candidate = Path.GetFullPath(Path.Combine(_rootPath, relative.Replace('/', Path.DirectorySeparatorChar)));
        var rootWithSeparator = _rootPath.EndsWith(Path.DirectorySeparatorChar)
            ? _rootPath
            : _rootPath + Path.DirectorySeparatorChar;
        if (!candidate.StartsWith(rootWithSeparator, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid private media storage key.", nameof(storageKey));
        return candidate;
    }

    private static string NormalizeScope(string scope)
    {
        if (string.IsNullOrWhiteSpace(scope) || Path.IsPathRooted(scope))
            throw new ArgumentException("Invalid private media scope.", nameof(scope));
        var parts = scope.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0 || parts.Any(part => part is "." or ".." || Path.GetFileName(part) != part))
            throw new ArgumentException("Invalid private media scope.", nameof(scope));
        return string.Join('/', parts);
    }

    private static string GetSafeExtension(string? fileName)
    {
        var extension = Path.GetExtension(fileName ?? string.Empty);
        return extension.Length is > 1 and <= 10 && extension[1..].All(char.IsLetterOrDigit)
            ? extension.ToLowerInvariant()
            : string.Empty;
    }
}
