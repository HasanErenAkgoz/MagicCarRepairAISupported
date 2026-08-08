using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Startup.HostedServices;

/// <summary>Deletes expired AI diagnosis drafts and their server-owned storage objects.</summary>
public sealed class AiDiagnosisMediaRetentionHostedService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<AiDiagnosisMediaRetentionHostedService> _logger;
    public AiDiagnosisMediaRetentionHostedService(IServiceProvider services, ILogger<AiDiagnosisMediaRetentionHostedService> logger) => (_services, _logger) = (services, logger);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try { await PurgeExpiredAsync(stoppingToken); }
            catch (Exception ex) { _logger.LogError(ex, "AI diagnosis media retention failed"); }
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    private async Task PurgeExpiredAsync(CancellationToken ct)
    {
        using var scope = _services.CreateScope();
        var assets = scope.ServiceProvider.GetRequiredService<IEntityRepository<MediaAsset, int>>();
        var storage = scope.ServiceProvider.GetRequiredService<IFileStorageService>();
        var expired = await assets.Query().Where(a => a.Purpose == "AiDiagnosisDraft" && a.ExpiresAt <= DateTime.UtcNow).ToListAsync(ct);
        foreach (var asset in expired)
        {
            if (!TryParseStorageKey(asset.StorageKey, out var container, out var file))
            {
                _logger.LogWarning("Expired AI asset {AssetId} has invalid storage key", asset.Id);
                continue;
            }
            try
            {
                // Delete storage first: retaining the DB row makes a failed storage cleanup retryable.
                await storage.DeleteFileAsync(file, container, ct);
                assets.Delete(asset);
            }
            catch (Exception ex) { _logger.LogError(ex, "Could not remove expired AI asset {AssetId}", asset.Id); }
        }
        await assets.SaveChangesAsync();
    }

    private static bool TryParseStorageKey(string? key, out string container, out string file)
    {
        container = file = string.Empty;
        var parts = key?.TrimStart('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts is null || parts.Length < 3 || !string.Equals(parts[0], "uploads", StringComparison.OrdinalIgnoreCase)) return false;
        container = string.Join('/', parts.Skip(1).Take(parts.Length - 2)); file = parts[^1];
        return true;
    }
}
