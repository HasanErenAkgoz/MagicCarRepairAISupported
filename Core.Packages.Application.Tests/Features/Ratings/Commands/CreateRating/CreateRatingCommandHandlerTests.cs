using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Ratings.Commands.CreateRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Ratings.Commands.CreateRating
{
    public class CreateRatingCommandHandlerTests
    {
        private readonly Mock<IServiceRatingRepository> _serviceRatingRepositoryMock;
        private readonly Mock<IWorkOrderRepository> _workOrderRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly CreateRatingCommandHandler _handler;

        public CreateRatingCommandHandlerTests()
        {
            _serviceRatingRepositoryMock = new Mock<IServiceRatingRepository>();
            _workOrderRepositoryMock = new Mock<IWorkOrderRepository>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = TestSupport.AutoMapperConfigurationFactory.Create(cfg =>
            {
                cfg.AddProfile<RatingMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateRatingCommandHandler(
                _serviceRatingRepositoryMock.Object,
                _workOrderRepositoryMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenWorkOrderNotFound()
        {
            // Arrange
            var clientId = 1;
            var command = new CreateRatingCommand
            {
                WorkOrderId = 999,
                Rating = 5,
                ServiceQuality = 5,
                PriceValue = 5,
                OnTimeDelivery = 5,
                StaffBehavior = 5
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrder?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenWorkOrderNotCompleted()
        {
            // Arrange
            var clientId = 1;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                Status = WorkOrderStatus.InProgress,
                CustomerId = 1
            };

            var command = new CreateRatingCommand
            {
                WorkOrderId = 1,
                Rating = 5,
                ServiceQuality = 5,
                PriceValue = 5,
                OnTimeDelivery = 5,
                StaffBehavior = 5
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRatingAlreadyExists()
        {
            // Arrange
            var clientId = 1;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                Status = WorkOrderStatus.Delivered,
                CustomerId = 1
            };

            var existingRating = new ServiceRating
            {
                Id = 1,
                WorkOrderId = 1
            };

            var command = new CreateRatingCommand
            {
                WorkOrderId = 1,
                Rating = 5,
                ServiceQuality = 5,
                PriceValue = 5,
                OnTimeDelivery = 5,
                StaffBehavior = 5
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);
            _serviceRatingRepositoryMock.Setup(x => x.GetByWorkOrderIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingRating);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRatingValueInvalid()
        {
            // Arrange
            var clientId = 1;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                Status = WorkOrderStatus.Delivered,
                CustomerId = 1
            };

            var command = new CreateRatingCommand
            {
                WorkOrderId = 1,
                Rating = 6, // Invalid: should be 1-5
                ServiceQuality = 5,
                PriceValue = 5,
                OnTimeDelivery = 5,
                StaffBehavior = 5
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);
            _serviceRatingRepositoryMock.Setup(x => x.GetByWorkOrderIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ServiceRating?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCreateRating_WhenValidCommand()
        {
            // Arrange
            var clientId = 1;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                Status = WorkOrderStatus.Delivered,
                CustomerId = 1
            };

            var command = new CreateRatingCommand
            {
                WorkOrderId = 1,
                Rating = 5,
                ServiceQuality = 4,
                PriceValue = 5,
                OnTimeDelivery = 4,
                StaffBehavior = 5,
                Comment = "Great service!"
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);
            _serviceRatingRepositoryMock.Setup(x => x.GetByWorkOrderIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ServiceRating?)null);
            _serviceRatingRepositoryMock.Setup(x => x.AddAsync(It.IsAny<ServiceRating>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ServiceRating entity, CancellationToken ct) =>
                {
                    entity.Id = 1;
                    entity.CreatedDate = DateTime.UtcNow;
                    return entity;
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.WorkOrderId.Should().Be(1);
            result.Rating.Should().Be(5);
            result.Status.Should().Be(RatingStatus.Pending);

            _serviceRatingRepositoryMock.Verify(x => x.AddAsync(
                It.Is<ServiceRating>(r =>
                    r.WorkOrderId == 1 &&
                    r.CustomerId == 1 &&
                    r.ClientId == clientId &&
                    r.Rating == 5 &&
                    r.ServiceQuality == 4 &&
                    r.Status == RatingStatus.Pending &&
                    r.Comment == "Great service!"),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
