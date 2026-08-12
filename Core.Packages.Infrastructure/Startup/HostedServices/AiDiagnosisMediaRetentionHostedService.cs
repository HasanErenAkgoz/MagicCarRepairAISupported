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
        var storage = scope.ServiceProvider.GetRequiredService<IPrivateMediaStorage>();
        var expired = await assets.Query().Where(a => a.Purpose == "AiDiagnosisDraft" && a.ExpiresAt <= DateTime.UtcNow).ToListAsync(ct);
        foreach (var asset in expired)
        {
            if (!IsPrivateStorageKey(asset.StorageKey))
            {
                _logger.LogWarning("Expired AI asset {AssetId} has invalid storage key", asset.Id);
                continue;
            }
            try
            {
                // Delete storage first: retaining the DB row makes a failed storage cleanup retryable.
                await storage.DeleteAsync(asset.StorageKey, ct);
                assets.Delete(asset);
            }
            catch (Exception ex) { _logger.LogError(ex, "Could not remove expired AI asset {AssetId}", asset.Id); }
        }
        await assets.SaveChangesAsync();

        var chatAttachments = scope.ServiceProvider.GetRequiredService<IEntityRepository<ChatAttachment, int>>();
        var expiredDrafts = await chatAttachments.Query()
            .Where(x => x.ChatMessageId == null && x.ExpiresAt <= DateTime.UtcNow)
            .ToListAsync(ct);
        foreach (var attachment in expiredDrafts)
        {
            if (!IsChatDraftStorageKey(attachment.StorageKey))
            {
                _logger.LogWarning("Expired chat attachment {AttachmentId} has invalid storage key", attachment.Id);
                continue;
            }

            try
            {
                // Delete storage first so failures remain retryable on the next run.
                await storage.DeleteAsync(attachment.StorageKey, ct);
                chatAttachments.Delete(attachment);
            }
            catch (Exception ex) { _logger.LogError(ex, "Could not remove expired chat attachment {AttachmentId}", attachment.Id); }
        }
        await chatAttachments.SaveChangesAsync();
    }

    private static bool IsPrivateStorageKey(string? key) =>
        PrivateMediaStorageKey.IsValid(key) && key!.StartsWith("private-media/ai-drafts/", StringComparison.Ordinal);

    private static bool IsChatDraftStorageKey(string? key) =>
        PrivateMediaStorageKey.IsValid(key) && key!.StartsWith("private-media/chat-drafts/", StringComparison.Ordinal);
}
