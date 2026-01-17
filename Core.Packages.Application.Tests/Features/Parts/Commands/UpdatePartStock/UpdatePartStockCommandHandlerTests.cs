using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePartStock;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Parts.Commands.UpdatePartStock
{
    public class UpdatePartStockCommandHandlerTests
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly Mock<IPartStockRepository> _partStockRepositoryMock;
        private readonly Mock<IStockMovementRepository> _stockMovementRepositoryMock;
        private readonly Mock<IStockAlertService> _stockAlertServiceMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly UpdatePartStockCommandHandler _handler;

        public UpdatePartStockCommandHandlerTests()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _partStockRepositoryMock = new Mock<IPartStockRepository>();
            _stockMovementRepositoryMock = new Mock<IStockMovementRepository>();
            _stockAlertServiceMock = new Mock<IStockAlertService>();
            _tenantServiceMock = new Mock<ITenantService>();

            _tenantServiceMock.Setup(x => x.GetCurrentClientId())
                .Returns(1);

            _handler = new UpdatePartStockCommandHandler(
                _partRepositoryMock.Object,
                _partStockRepositoryMock.Object,
                _stockMovementRepositoryMock.Object,
                _stockAlertServiceMock.Object,
                _tenantServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPartNotFound()
        {
            // Arrange
            var command = new UpdatePartStockCommand
            {
                PartId = 1,
                Quantity = 10,
                MovementType = StockMovementType.StockIn
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldUpdateStock_WhenValidStockIn()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                MinimumStockLevel = 10,
                IsLowStockAlertEnabled = true,
                ClientId = 1,
                Stock = new PartStock
                {
                    Id = 1,
                    PartId = 1,
                    Quantity = 5,
                    Location = "A1"
                }
            };

            var command = new UpdatePartStockCommand
            {
                PartId = 1,
                Quantity = 15,
                Location = "A1"
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);

            _partStockRepositoryMock.Setup(x => x.GetByPartIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part.Stock);

            _partStockRepositoryMock.Setup(x => x.Update(It.IsAny<PartStock>()));
            _partStockRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            _stockMovementRepositoryMock.Setup(x => x.AddAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((StockMovement entity, CancellationToken ct) => entity);
            _stockMovementRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            _stockAlertServiceMock.Setup(x => x.CheckAndCreateAlertAsync(1, 1, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Quantity.Should().Be(15);
            result.PartId.Should().Be(1);
            result.StockId.Should().Be(1);

            _partStockRepositoryMock.Verify(x => x.Update(It.IsAny<PartStock>()), Times.Once);
            _stockMovementRepositoryMock.Verify(x => x.AddAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCreateStock_WhenStockDoesNotExist()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                MinimumStockLevel = 10,
                IsLowStockAlertEnabled = true,
                ClientId = 1
            };

            var command = new UpdatePartStockCommand
            {
                PartId = 1,
                Quantity = 50,
                Location = "A1"
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);

            _partStockRepositoryMock.Setup(x => x.GetByPartIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartStock?)null);

            _partStockRepositoryMock.Setup(x => x.AddAsync(It.IsAny<PartStock>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartStock entity, CancellationToken ct) => entity);

            _partStockRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            _stockMovementRepositoryMock.Setup(x => x.AddAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((StockMovement entity, CancellationToken ct) => entity);
            _stockMovementRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            _stockAlertServiceMock.Setup(x => x.CheckAndCreateAlertAsync(1, It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Quantity.Should().Be(50);
            result.PartId.Should().Be(1);

            _partStockRepositoryMock.Verify(x => x.AddAsync(It.IsAny<PartStock>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

