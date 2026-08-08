using System.Security.Claims;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.AI.Commands.Diagnose;
using MagicCarRepairAISupported.Application.Features.AI.Commands.UploadDiagnosisAsset;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;

namespace MagicCarRepairAISupported.Application.Tests.Features.AI.Diagnose;

public class DiagnoseMediaAssetAuthorizationTests
{
    [Theory]
    [InlineData(2, 7, 3)] // other tenant
    [InlineData(1, 8, 1)] // other owner
    public async Task RejectsAssetOutsideAuthenticatedTenantOrOwner(int clientId, int ownerId, int currentClient)
    {
        var asset = NewAsset(clientId, ownerId, DateTime.UtcNow.AddHours(1));
        var (handler, _, storage) = CreateHandler(asset, currentClient);
        var result = await handler.Handle(new DiagnoseCommand { Complaint = "hasar", MediaAssetIds = [1] }, CancellationToken.None);
        Assert.False(result.Success);
        storage.Verify(x => x.GetFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task RejectsExpiredAssetBeforeStorageIsRead()
    {
        var (handler, _, storage) = CreateHandler(NewAsset(1, 7, DateTime.UtcNow.AddMinutes(-1)), 1);
        var result = await handler.Handle(new DiagnoseCommand { Complaint = "hasar", MediaAssetIds = [1] }, CancellationToken.None);
        Assert.False(result.Success);
        storage.Verify(x => x.GetFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task ValidOwnerAsset_IsReadAndPassedAsBinaryImage()
    {
        var (handler, ai, storage) = CreateHandler(NewAsset(1, 7, DateTime.UtcNow.AddHours(1)), 1);
        storage.Setup(x => x.GetFileAsync("a.jpg", "ai-drafts/1/7")).ReturnsAsync(new MemoryStream([1, 2, 3]));
        ai.Setup(x => x.DiagnoseFromTextAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<List<DiagnosisImage>>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DiagnosisResultDto());
        var result = await handler.Handle(new DiagnoseCommand { Complaint = "hasar", MediaAssetIds = [1] }, CancellationToken.None);
        Assert.True(result.Success);
        ai.Verify(x => x.DiagnoseFromTextAsync(It.IsAny<string>(), It.IsAny<int?>(), It.Is<List<DiagnosisImage>>(i => i.Count == 1 && i[0].Bytes.Length == 3), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    private static MediaAsset NewAsset(int clientId, int ownerId, DateTime expiry) => new() { Id = 1, ClientId = clientId, OwnerUserId = ownerId, Purpose = UploadDiagnosisAssetCommandHandler.Purpose, StorageKey = "/uploads/ai-drafts/1/7/a.jpg", ContentType = "image/jpeg", Length = 3, ExpiresAt = expiry };
    private static (DiagnoseCommandHandler handler, Mock<IAIDiagnosisService> ai, Mock<IFileStorageService> storage) CreateHandler(MediaAsset asset, int clientId)
    {
        var ai = new Mock<IAIDiagnosisService>(); var assets = new Mock<IEntityRepository<MediaAsset, int>>(); var tenants = new Mock<ITenantService>(); var storage = new Mock<IFileStorageService>(); var http = new Mock<IHttpContextAccessor>();
        assets.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(asset); tenants.Setup(x => x.GetRequiredClientId()).Returns(clientId);
        http.SetupGet(x => x.HttpContext).Returns(new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "7")], "test")) });
        return (new DiagnoseCommandHandler(ai.Object, assets.Object, tenants.Object, http.Object, storage.Object), ai, storage);
    }
}
