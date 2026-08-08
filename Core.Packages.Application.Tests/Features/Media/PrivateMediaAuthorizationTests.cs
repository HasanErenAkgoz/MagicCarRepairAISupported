using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateQuoteRequestPhoto;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateWorkOrderPhoto;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Moq;

namespace MagicCarRepairAISupported.Application.Tests.Features.Media;

public class PrivateMediaAuthorizationTests
{
    [Fact]
    public async Task WorkOrderPhoto_RejectsPhotoFromAnotherTenant_BeforeStorageIsRead()
    {
        var photos = new Mock<IEntityRepository<WorkOrderPhoto, int>>();
        var tenants = new Mock<ITenantService>();
        var files = new Mock<IFileStorageService>();
        tenants.Setup(x => x.GetRequiredClientId()).Returns(10);
        photos.Setup(x => x.GetByIdAsync(7, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new WorkOrderPhoto { Id = 7, WorkOrderId = 4, ClientId = 11, FilePath = "/uploads/work-orders/4/private.jpg" });
        var handler = new GetPrivateWorkOrderPhotoQueryHandler(photos.Object, tenants.Object, files.Object);

        await Assert.ThrowsAsync<DomainException>(() => handler.Handle(new() { WorkOrderId = 4, PhotoId = 7 }, CancellationToken.None));
        files.Verify(x => x.GetFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task QuoteRequestPhoto_RejectsIndexOutsideServerStoredPhotoList()
    {
        var requests = new Mock<IQuoteRequestRepository>();
        var tenants = new Mock<ITenantService>();
        var files = new Mock<IFileStorageService>();
        tenants.Setup(x => x.GetRequiredClientId()).Returns(10);
        requests.Setup(x => x.GetByIdAsync(8, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new QuoteRequest { Id = 8, ClientId = 10, PhotoPaths = "[\"/uploads/quotes/a.jpg\"]" });
        var handler = new GetPrivateQuoteRequestPhotoQueryHandler(requests.Object, tenants.Object, files.Object);

        await Assert.ThrowsAsync<DomainException>(() => handler.Handle(new() { QuoteRequestId = 8, PhotoId = 2 }, CancellationToken.None));
        files.Verify(x => x.GetFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}
