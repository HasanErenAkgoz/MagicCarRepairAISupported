using System.Security.Claims;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.UploadDiagnosisAsset;

public sealed class UploadDiagnosisAssetCommandHandler : IRequestHandler<UploadDiagnosisAssetCommand, IDataResult<UploadDiagnosisAssetResponse>>
{
    public const string Purpose = "AiDiagnosisDraft";
    private static readonly HashSet<string> Allowed = ["image/jpeg", "image/png", "image/webp"];
    private const long MaxBytes = 10 * 1024 * 1024;
    private readonly IEntityRepository<MediaAsset, int> _assets; private readonly IPrivateMediaStorage _storage; private readonly ITenantService _tenants; private readonly IHttpContextAccessor _http;
    public UploadDiagnosisAssetCommandHandler(IEntityRepository<MediaAsset, int> assets, IPrivateMediaStorage storage, ITenantService tenants, IHttpContextAccessor http) => (_assets,_storage,_tenants,_http)=(assets,storage,tenants,http);
    public async Task<IDataResult<UploadDiagnosisAssetResponse>> Handle(UploadDiagnosisAssetCommand request, CancellationToken ct)
    {
        var userId = int.TryParse(_http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;
        if (userId == 0 || request.File is null || request.File.Length is <= 0 or > MaxBytes || !Allowed.Contains(request.File.ContentType)) return new ErrorDataResult<UploadDiagnosisAssetResponse>("Invalid diagnosis image.");
        var clientId = _tenants.GetRequiredClientId(); var key = await _storage.StoreAsync(request.File, $"ai-drafts/{clientId}/{userId}", ct);
        var asset = new MediaAsset { ClientId=clientId, OwnerUserId=userId, Purpose=Purpose, StorageKey=key, ContentType=request.File.ContentType, Length=request.File.Length, ExpiresAt=DateTime.UtcNow.AddHours(24), CreatedDate=DateTime.UtcNow, CreatedBy=userId };
        await _assets.AddAsync(asset, ct); await _assets.SaveChangesAsync();
        return new SuccessDataResult<UploadDiagnosisAssetResponse>(new UploadDiagnosisAssetResponse { AssetId=asset.Id, ExpiresAt=asset.ExpiresAt });
    }
}
