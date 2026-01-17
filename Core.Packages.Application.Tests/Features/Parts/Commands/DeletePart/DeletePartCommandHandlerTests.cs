using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.DeletePart;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Parts.Commands.DeletePart
{
    public class DeletePartCommandHandlerTests
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly DeletePartCommandHandler _handler;

        public DeletePartCommandHandlerTests()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _handler = new DeletePartCommandHandler(_partRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPartNotFound()
        {
            // Arrange
            var command = new DeletePartCommand { Id = 1 };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPartUsedInActiveWorkOrders()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                ClientId = 1
            };

            var command = new DeletePartCommand { Id = 1 };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);

            _partRepositoryMock.Setup(x => x.IsPartUsedInActiveWorkOrdersAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true); // Used in active work orders

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldDeletePart_WhenValidCommand()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                ClientId = 1,
                Status = Status.Active
            };

            var command = new DeletePartCommand { Id = 1 };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);

            _partRepositoryMock.Setup(x => x.IsPartUsedInActiveWorkOrdersAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false); // Not used in active work orders

            _partRepositoryMock.Setup(x => x.Update(It.IsAny<Part>()));
            _partRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Part deleted successfully");
            part.Status.Should().Be(Status.Deleted);

            _partRepositoryMock.Verify(x => x.Update(It.IsAny<Part>()), Times.Once);
            _partRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}


