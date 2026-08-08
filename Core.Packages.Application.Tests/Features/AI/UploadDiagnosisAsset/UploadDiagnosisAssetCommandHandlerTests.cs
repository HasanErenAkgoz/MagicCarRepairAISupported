using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.AI.Commands.UploadDiagnosisAsset;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Tests.Features.AI.UploadDiagnosisAsset;

public class UploadDiagnosisAssetCommandHandlerTests
{
    [Fact]
    public async Task RejectsUnsupportedContentTypeBeforeStorageIsCalled()
    {
        var assets = new Mock<IEntityRepository<MediaAsset, int>>();
        var storage = new Mock<IFileStorageService>();
        var tenants = new Mock<ITenantService>();
        var http = new Mock<IHttpContextAccessor>();
        var handler = new UploadDiagnosisAssetCommandHandler(assets.Object, storage.Object, tenants.Object, http.Object);
        var file = new Mock<IFormFile>();
        file.SetupGet(x => x.Length).Returns(100);
        file.SetupGet(x => x.ContentType).Returns("text/plain");

        var result = await handler.Handle(new UploadDiagnosisAssetCommand { File = file.Object }, CancellationToken.None);

        Assert.False(result.Success);
        storage.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ValidUpload_PersistsThePurposeRequiredByDiagnoseValidation()
    {
        var assets = new Mock<IEntityRepository<MediaAsset, int>>();
        var storage = new Mock<IFileStorageService>();
        var tenants = new Mock<ITenantService>();
        var http = new Mock<IHttpContextAccessor>();
        MediaAsset? captured = null;
        var context = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "7")], "test")) };
        http.SetupGet(x => x.HttpContext).Returns(context);
        tenants.Setup(x => x.GetRequiredClientId()).Returns(3);
        storage.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync("/uploads/ai-drafts/3/7/a.jpg");
        assets.Setup(x => x.AddAsync(It.IsAny<MediaAsset>(), It.IsAny<CancellationToken>())).Callback<MediaAsset, CancellationToken>((a, _) => captured = a).ReturnsAsync((MediaAsset a, CancellationToken _) => a);
        var file = new Mock<IFormFile>(); file.SetupGet(x => x.Length).Returns(10); file.SetupGet(x => x.ContentType).Returns("image/jpeg");
        var handler = new UploadDiagnosisAssetCommandHandler(assets.Object, storage.Object, tenants.Object, http.Object);

        var result = await handler.Handle(new UploadDiagnosisAssetCommand { File = file.Object }, CancellationToken.None);

        Assert.True(result.Success);
        Assert.NotNull(captured);
        Assert.Equal(UploadDiagnosisAssetCommandHandler.Purpose, captured!.Purpose);
    }
}
