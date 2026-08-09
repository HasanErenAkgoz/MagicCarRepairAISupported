using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.AI.Commands.UploadDiagnosisAsset;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.Diagnose
{
    public class DiagnoseCommandHandler : IRequestHandler<DiagnoseCommand, IDataResult<DiagnosisResultDto>>
    {
        private readonly IAIDiagnosisService _aiDiagnosisService;
        private readonly IEntityRepository<MediaAsset, int> _assets;
        private readonly ITenantService _tenants;
        private readonly IHttpContextAccessor _http;
        private readonly IPrivateMediaStorage _storage;

        public DiagnoseCommandHandler(IAIDiagnosisService aiDiagnosisService, IEntityRepository<MediaAsset, int> assets, ITenantService tenants, IHttpContextAccessor http, IPrivateMediaStorage storage)
        {
            _aiDiagnosisService = aiDiagnosisService;
            _assets = assets; _tenants = tenants; _http = http; _storage = storage;
        }

        public async Task<IDataResult<DiagnosisResultDto>> Handle(DiagnoseCommand request, CancellationToken cancellationToken)
        {
            DiagnosisResultDto result;

            if (request.IsVoice && request.VoiceData != null && request.VoiceData.Length > 0)
            {
                result = await _aiDiagnosisService.DiagnoseFromVoiceAsync(request.VoiceData, request.VehicleId, cancellationToken);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.Complaint))
                {
                    return new ErrorDataResult<DiagnosisResultDto>("Şikayet metni boş olamaz");
                }

                List<DiagnosisImage>? images = null;
                if (request.MediaAssetIds?.Count > 0)
                {
                    var userId = int.TryParse(_http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;
                    var clientId = _tenants.GetRequiredClientId();
                    if (request.MediaAssetIds.Distinct().Count() > 5) return new ErrorDataResult<DiagnosisResultDto>("At most five diagnosis assets are allowed.");
                    var assets = new List<MediaAsset>();
                    foreach (var assetId in request.MediaAssetIds.Distinct())
                    {
                        var asset = await _assets.GetByIdAsync(assetId, cancellationToken);
                        if (asset is not null) assets.Add(asset);
                    }
                    var validCount = assets.Count(a => a.ClientId == clientId && a.OwnerUserId == userId && a.Purpose == UploadDiagnosisAssetCommandHandler.Purpose && a.ExpiresAt > DateTime.UtcNow);
                    if (userId == 0 || validCount != request.MediaAssetIds.Distinct().Count())
                        return new ErrorDataResult<DiagnosisResultDto>("One or more diagnosis assets are invalid or expired.");
                    images = new();
                    foreach (var asset in assets)
                    {
                        if (!IsPrivateStorageKey(asset.StorageKey) || asset.Length > 10 * 1024 * 1024) return new ErrorDataResult<DiagnosisResultDto>("Invalid diagnosis asset.");
                        await using var stream = await _storage.OpenReadAsync(asset.StorageKey, cancellationToken);
                        if (stream is null) return new ErrorDataResult<DiagnosisResultDto>("Diagnosis asset is unavailable.");
                        using var memory = new MemoryStream(); await stream.CopyToAsync(memory, cancellationToken);
                        if (memory.Length != asset.Length) return new ErrorDataResult<DiagnosisResultDto>("Diagnosis asset size mismatch.");
                        images.Add(new DiagnosisImage(memory.ToArray(), asset.ContentType));
                    }
                }

                // Only validated, bounded server-owned bytes reach the provider adapter.
                result = await _aiDiagnosisService.DiagnoseFromTextAsync(request.Complaint, request.VehicleId, images, request.Language, cancellationToken);
            }

            return new SuccessDataResult<DiagnosisResultDto>(result, "Arıza tespiti başarıyla tamamlandı");
        }

        private static bool IsPrivateStorageKey(string? key) =>
            PrivateMediaStorageKey.IsValid(key) && key!.StartsWith("private-media/ai-drafts/", StringComparison.Ordinal);
    }
}
