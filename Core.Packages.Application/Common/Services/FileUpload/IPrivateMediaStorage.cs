using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Common.Services.FileUpload;

/// <summary>
/// Stores server-owned media that must never be addressable through the web root.
/// Returned keys are opaque storage references, not URLs, and are only suitable
/// for authorized server-side reads and deletes.
/// </summary>
public interface IPrivateMediaStorage
{
    Task<string> StoreAsync(IFormFile file, string scope, CancellationToken cancellationToken);
    Task<Stream?> OpenReadAsync(string storageKey, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(string storageKey, CancellationToken cancellationToken);
}
