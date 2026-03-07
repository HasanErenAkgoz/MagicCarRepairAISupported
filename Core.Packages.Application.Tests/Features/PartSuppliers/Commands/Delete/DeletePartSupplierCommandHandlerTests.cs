using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Delete;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.PartSuppliers.Commands.Delete
{
    public class DeletePartSupplierCommandHandlerTests
    {
        private readonly Mock<IPartSupplierRepository> _partSupplierRepositoryMock;
        private readonly DeletePartSupplierCommandHandler _handler;

        public DeletePartSupplierCommandHandlerTests()
        {
            _partSupplierRepositoryMock = new Mock<IPartSupplierRepository>();
            _handler = new DeletePartSupplierCommandHandler(_partSupplierRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSupplierNotFound()
        {
            // Arrange
            var command = new DeletePartSupplierCommand { Id = 1 };

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartSupplier?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldDeleteSupplier_WhenValidCommand()
        {
            // Arrange
            var supplier = new PartSupplier
            {
                Id = 1,
                CompanyName = "Test Supplier",
                IsActive = true,
                Status = Status.Active,
                ClientId = 1
            };

            var command = new DeletePartSupplierCommand { Id = 1 };

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(supplier);

            _partSupplierRepositoryMock.Setup(x => x.Update(It.IsAny<PartSupplier>()));
            _partSupplierRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Supplier deleted successfully");
            supplier.Status.Should().Be(Status.Deleted);
            supplier.IsActive.Should().BeFalse();

            _partSupplierRepositoryMock.Verify(x => x.Update(It.IsAny<PartSupplier>()), Times.Once);
            _partSupplierRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}


