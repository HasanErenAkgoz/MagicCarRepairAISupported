using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileList;
using MagicCarRepairAISupported.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MagicCarRepairAISupported.WebAPI.Tests.Controllers
{
    public class WorkOrdersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly WorkOrdersController _controller;

        public WorkOrdersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new WorkOrdersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnMobileContract_ByDefault()
        {
            var response = new GetMobileWorkOrdersListResponse
            {
                Data = new List<WorkOrderListItem>
                {
                    new()
                    {
                        Id = "1",
                        OrderNo = "WO-2024-001",
                        CustomerName = "Test Customer",
                        VehiclePlate = "34 ABC 123",
                        VehicleModel = "Toyota Corolla 2020",
                        Service = "Genel Bakim",
                        Status = "pending",
                        Date = "2024-03-09",
                        Amount = 1500m,
                        TechnicianName = "Test Tech"
                    }
                },
                TotalCount = 1,
                Page = 1,
                PageSize = 20
            };

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetMobileWorkOrdersListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _controller.GetAll(null, null, null, null, null, null, null, null, null, false);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().NotBeNull();
            okResult.Value!.Should().BeEquivalentTo(new
            {
                success = true,
                data = response.Data,
                totalCount = response.TotalCount,
                page = response.Page,
                pageSize = response.PageSize,
                totalPages = response.TotalPages
            });

            _mediatorMock.Verify(
                x => x.Send(It.IsAny<GetMobileWorkOrdersListQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
            _mediatorMock.Verify(
                x => x.Send(It.IsAny<GetAllWorkOrdersQuery>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task GetById_ShouldReturnMobileContract_ByDefault()
        {
            var response = new GetMobileWorkOrderDetailResponse
            {
                Id = "1",
                OrderNo = "WO-2024-001",
                Status = "inProgress",
                CreatedAt = "2024-03-09T08:00:00Z",
                UpdatedAt = "2024-03-09T10:00:00Z"
            };

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetMobileWorkOrderDetailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _controller.GetById("1", false);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(new
            {
                success = true,
                data = response
            });

            _mediatorMock.Verify(
                x => x.Send(It.IsAny<GetMobileWorkOrderDetailQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
            _mediatorMock.Verify(
                x => x.Send(It.IsAny<GetWorkOrderByIdQuery>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task GetById_ShouldReturnLegacyContract_WhenRequested()
        {
            var response = new GetWorkOrderByIdResponse
            {
                Id = 1,
                WorkOrderNumber = "WO-2024-001"
            };

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetWorkOrderByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = await _controller.GetById("1", true);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeSameAs(response);

            _mediatorMock.Verify(
                x => x.Send(It.IsAny<GetWorkOrderByIdQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
            _mediatorMock.Verify(
                x => x.Send(It.IsAny<GetMobileWorkOrderDetailQuery>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
