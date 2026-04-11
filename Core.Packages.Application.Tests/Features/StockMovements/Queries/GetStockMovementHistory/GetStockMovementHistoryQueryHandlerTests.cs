using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.StockMovements.Queries.GetStockMovementHistory;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.StockMovements.Queries.GetStockMovementHistory
{
    public class GetStockMovementHistoryQueryHandlerTests
    {
        private readonly Mock<IStockMovementRepository> _stockMovementRepositoryMock;
        private readonly GetStockMovementHistoryQueryHandler _handler;

        public GetStockMovementHistoryQueryHandlerTests()
        {
            _stockMovementRepositoryMock = new Mock<IStockMovementRepository>();
            _handler = new GetStockMovementHistoryQueryHandler(_stockMovementRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoMovements()
        {
            // Arrange
            var query = new GetStockMovementHistoryQuery();

            _stockMovementRepositoryMock.Setup(x => x.GetAllWithDetailsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<StockMovement>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
        }

        [Fact]
        public async Task Handle_ShouldReturnMovements_WhenFilteredByPartId()
        {
            // Arrange
            var movements = new List<StockMovement>
            {
                new StockMovement
                {
                    Id = 1,
                    PartId = 1,
                    MovementType = StockMovementType.In,
                    Quantity = 10,
                    MovementDate = DateTime.UtcNow
                },
                new StockMovement
                {
                    Id = 2,
                    PartId = 1,
                    MovementType = StockMovementType.Out,
                    Quantity = 5,
                    MovementDate = DateTime.UtcNow.AddDays(-1)
                }
            };

            var query = new GetStockMovementHistoryQuery
            {
                PartId = 1
            };

            _stockMovementRepositoryMock.Setup(x => x.GetByPartIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(movements);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalCount.Should().Be(2);
        }

        [Fact]
        public async Task Handle_ShouldReturnMovements_WhenFilteredByMovementType()
        {
            // Arrange
            var movements = new List<StockMovement>
            {
                new StockMovement
                {
                    Id = 1,
                    PartId = 1,
                    MovementType = StockMovementType.In,
                    Quantity = 10,
                    MovementDate = DateTime.UtcNow
                }
            };

            var query = new GetStockMovementHistoryQuery
            {
                MovementType = StockMovementType.In
            };

            _stockMovementRepositoryMock.Setup(x => x.GetByMovementTypeAsync(StockMovementType.In, It.IsAny<CancellationToken>()))
                .ReturnsAsync(movements);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.Items.First().MovementType.Should().Be(StockMovementType.In);
        }

        [Fact]
        public async Task Handle_ShouldReturnMovements_WhenFilteredByDateRange()
        {
            // Arrange
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;

            var movements = new List<StockMovement>
            {
                new StockMovement
                {
                    Id = 1,
                    PartId = 1,
                    MovementType = StockMovementType.In,
                    Quantity = 10,
                    MovementDate = DateTime.UtcNow.AddDays(-3)
                }
            };

            var query = new GetStockMovementHistoryQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            _stockMovementRepositoryMock.Setup(x => x.GetByDateRangeAsync(startDate, endDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(movements);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
        }
    }
}




