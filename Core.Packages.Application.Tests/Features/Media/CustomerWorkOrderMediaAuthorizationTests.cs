using System.Security.Claims;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetCustomerWorkOrderPhoto;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Tests.Features.Media;

public class CustomerWorkOrderMediaAuthorizationTests
{
    [Fact]
    public async Task RejectsPhotoThatIsNotAssociatedWithOwnedWorkOrder_BeforeReadingStorage()
    {
        var workOrders = new Mock<IWorkOrderRepository>();
        var photos = new Mock<IEntityRepository<WorkOrderPhoto, int>>();
        var customers = new Mock<ICustomerRepository>();
        var tenants = new Mock<ITenantService>();
        var files = new Mock<IFileStorageService>();
        var http = new Mock<IHttpContextAccessor>();
        http.SetupGet(x => x.HttpContext).Returns(new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity([new Claim("UserType", "4")], "test")) });
        workOrders.Setup(x => x.GetByIdAsync(20, It.IsAny<CancellationToken>())).ReturnsAsync(new WorkOrder { Id = 20, CustomerId = 3, ClientId = 9 });
        photos.Setup(x => x.GetByIdAsync(7, It.IsAny<CancellationToken>())).ReturnsAsync(new WorkOrderPhoto { Id = 7, WorkOrderId = 21, ClientId = 9, FilePath = "/uploads/work-orders/21/a.jpg" });
        var handler = new GetCustomerWorkOrderPhotoQueryHandler(workOrders.Object, photos.Object, customers.Object, tenants.Object, http.Object, files.Object);

        await Assert.ThrowsAsync<DomainException>(() => handler.Handle(new() { CustomerId = 3, WorkOrderId = 20, PhotoId = 7 }, CancellationToken.None));
        files.Verify(x => x.GetFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}
