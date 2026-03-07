using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Ratings.Commands.ModerateRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Ratings.Commands.ModerateRating
{
    public class ModerateRatingCommandHandlerTests
    {
        private readonly Mock<IServiceRatingRepository> _serviceRatingRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly ModerateRatingCommandHandler _handler;

        public ModerateRatingCommandHandlerTests()
        {
            _serviceRatingRepositoryMock = new Mock<IServiceRatingRepository>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RatingMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new ModerateRatingCommandHandler(
                _serviceRatingRepositoryMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRatingNotFound()
        {
            // Arrange
            var clientId = 1;
            var command = new ModerateRatingCommand
            {
                RatingId = 999,
                Status = RatingStatus.Approved
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _serviceRatingRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((ServiceRating?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRatingNotBelongToClient()
        {
            // Arrange
            var clientId = 1;
            var otherClientId = 2;

            var rating = new ServiceRating
            {
                Id = 1,
                ClientId = otherClientId
            };

            var command = new ModerateRatingCommand
            {
                RatingId = 1,
                Status = RatingStatus.Approved
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _serviceRatingRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(rating);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRejectionReasonMissing()
        {
            // Arrange
            var clientId = 1;

            var rating = new ServiceRating
            {
                Id = 1,
                ClientId = clientId,
                Status = RatingStatus.Pending
            };

            var command = new ModerateRatingCommand
            {
                RatingId = 1,
                Status = RatingStatus.Rejected,
                RejectionReason = null // Missing rejection reason
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _serviceRatingRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(rating);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldApproveRating_WhenValidCommand()
        {
            // Arrange
            var clientId = 1;

            var rating = new ServiceRating
            {
                Id = 1,
                ClientId = clientId,
                Status = RatingStatus.Pending
            };

            var command = new ModerateRatingCommand
            {
                RatingId = 1,
                Status = RatingStatus.Approved
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _serviceRatingRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(rating);
            _serviceRatingRepositoryMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(RatingStatus.Approved);
            rating.Status.Should().Be(RatingStatus.Approved);

            _serviceRatingRepositoryMock.Verify(x => x.Update(It.IsAny<ServiceRating>()), Times.Once);
            _serviceRatingRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldRejectRating_WithReason()
        {
            // Arrange
            var clientId = 1;

            var rating = new ServiceRating
            {
                Id = 1,
                ClientId = clientId,
                Status = RatingStatus.Pending
            };

            var command = new ModerateRatingCommand
            {
                RatingId = 1,
                Status = RatingStatus.Rejected,
                RejectionReason = "Inappropriate content"
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _serviceRatingRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(rating);
            _serviceRatingRepositoryMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(RatingStatus.Rejected);

            _serviceRatingRepositoryMock.Verify(x => x.Update(It.IsAny<ServiceRating>()), Times.Once);
            _serviceRatingRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}

