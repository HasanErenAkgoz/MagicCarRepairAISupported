using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.UploadDiagnosisAsset;

public sealed class UploadDiagnosisAssetCommand : IRequest<IDataResult<UploadDiagnosisAssetResponse>>
{
    public IFormFile File { get; set; } = null!;
}
public sealed class UploadDiagnosisAssetResponse { public int AssetId { get; set; } public DateTime ExpiresAt { get; set; } }
