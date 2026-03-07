using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Application.Features.StockMovements.Commands.RecordStockMovement;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.StockMovements.Commands.RecordStockMovement
{
    public class RecordStockMovementCommandHandlerTests
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly Mock<IPartStockRepository> _partStockRepositoryMock;
        private readonly Mock<IStockMovementRepository> _stockMovementRepositoryMock;
        private readonly Mock<IStockAlertService> _stockAlertServiceMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly RecordStockMovementCommandHandler _handler;

        public RecordStockMovementCommandHandlerTests()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _partStockRepositoryMock = new Mock<IPartStockRepository>();
            _stockMovementRepositoryMock = new Mock<IStockMovementRepository>();
            _stockAlertServiceMock = new Mock<IStockAlertService>();
            _tenantServiceMock = new Mock<ITenantService>();

            _tenantServiceMock.Setup(x => x.GetCurrentClientId())
                .Returns(1);

            _handler = new RecordStockMovementCommandHandler(
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
            var command = new RecordStockMovementCommand
            {
                PartId = 1,
                MovementType = StockMovementType.In,
                Quantity = 10
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInsufficientStock()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                ClientId = 1,
                Stock = new PartStock
                {
                    Id = 1,
                    PartId = 1,
                    Quantity = 5
                }
            };

            var command = new RecordStockMovementCommand
            {
                PartId = 1,
                MovementType = StockMovementType.Out,
                Quantity = 10 // More than available
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);

            _partStockRepositoryMock.Setup(x => x.GetByPartIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part.Stock);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldRecordStockIn_WhenValid()
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
                    Quantity = 5
                }
            };

            var command = new RecordStockMovementCommand
            {
                PartId = 1,
                MovementType = StockMovementType.In,
                Quantity = 10,
                EmployeeId = 1,
                Description = "Stock entry",
                ReferenceNumber = "REF-001"
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
                .ReturnsAsync((StockAlert?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.PartId.Should().Be(1);
            result.PartCode.Should().Be("PART-001");
            result.MovementType.Should().Be(StockMovementType.In);
            result.Quantity.Should().Be(10);
            result.PreviousStock.Should().Be(5);
            result.CurrentStock.Should().Be(15); // 5 + 10

            _partStockRepositoryMock.Verify(x => x.Update(It.IsAny<PartStock>()), Times.Once);
            _stockMovementRepositoryMock.Verify(x => x.AddAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldRecordStockOut_WhenValid()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                ClientId = 1,
                Stock = new PartStock
                {
                    Id = 1,
                    PartId = 1,
                    Quantity = 20
                }
            };

            var command = new RecordStockMovementCommand
            {
                PartId = 1,
                MovementType = StockMovementType.Out,
                Quantity = 5,
                EmployeeId = 1
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

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.MovementType.Should().Be(StockMovementType.Out);
            result.Quantity.Should().Be(5);
            result.PreviousStock.Should().Be(20);
            result.CurrentStock.Should().Be(15); // 20 - 5
        }
    }
}

