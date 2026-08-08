using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivatePartPhoto;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Moq;

namespace MagicCarRepairAISupported.Application.Tests.Features.Media;

public class PartMediaAuthorizationTests
{
    [Fact]
    public async Task RejectsPhotoFromAnotherTenant_BeforeStorageIsRead()
    {
        var parts = new Mock<IPartRepository>();
        var photos = new Mock<IEntityRepository<PartPhoto, int>>();
        var tenants = new Mock<ITenantService>();
        var files = new Mock<IFileStorageService>();
        tenants.Setup(x => x.GetRequiredClientId()).Returns(4);
        parts.Setup(x => x.GetByIdAsync(12, It.IsAny<CancellationToken>())).ReturnsAsync(new Part { Id = 12, ClientId = 4 });
        photos.Setup(x => x.GetByIdAsync(8, It.IsAny<CancellationToken>())).ReturnsAsync(new PartPhoto { Id = 8, PartId = 12, ClientId = 5, FilePath = "/uploads/parts/12/private.jpg" });
        var handler = new GetPrivatePartPhotoQueryHandler(parts.Object, photos.Object, tenants.Object, files.Object);

        await Assert.ThrowsAsync<DomainException>(() => handler.Handle(new() { PartId = 12, PhotoId = 8 }, CancellationToken.None));
        files.Verify(x => x.GetFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}
