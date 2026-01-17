using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.Parts.Profiles;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetLowStockParts;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Parts.Queries.GetLowStockParts
{
    public class GetLowStockPartsQueryHandlerTests
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetLowStockPartsQueryHandler _handler;

        public GetLowStockPartsQueryHandlerTests()
        {
            _partRepositoryMock = new Mock<IPartRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PartMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetLowStockPartsQueryHandler(_partRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoLowStockParts()
        {
            // Arrange
            var query = new GetLowStockPartsQuery();

            _partRepositoryMock.Setup(x => x.GetLowStockPartsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Part>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
        }

        [Fact]
        public async Task Handle_ShouldReturnLowStockParts_WhenPartsExist()
        {
            // Arrange
            var lowStockParts = new List<Part>
            {
                new Part
                {
                    Id = 1,
                    PartCode = "PART-001",
                    Name = "Low Stock Part 1",
                    Category = PartCategory.Engine,
                    BrandType = PartBrandType.Original,
                    MinimumStockLevel = 10,
                    IsLowStockAlertEnabled = true,
                    ClientId = 1,
                    Stock = new PartStock { Quantity = 5, Location = "A1" },
                    Supplier = new PartSupplier { CompanyName = "Supplier 1" }
                },
                new Part
                {
                    Id = 2,
                    PartCode = "PART-002",
                    Name = "Low Stock Part 2",
                    Category = PartCategory.BrakeSystem,
                    BrandType = PartBrandType.Aftermarket,
                    MinimumStockLevel = 20,
                    IsLowStockAlertEnabled = true,
                    ClientId = 1,
                    Stock = new PartStock { Quantity = 15, Location = "B2" },
                    Supplier = new PartSupplier { CompanyName = "Supplier 2" }
                }
            };

            var query = new GetLowStockPartsQuery();

            _partRepositoryMock.Setup(x => x.GetLowStockPartsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(lowStockParts);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalCount.Should().Be(2);

            var firstItem = result.Items.First();
            firstItem.Id.Should().Be(1);
            firstItem.PartCode.Should().Be("PART-001");
            firstItem.Name.Should().Be("Low Stock Part 1");
            firstItem.CurrentStock.Should().Be(5);
            firstItem.MinimumStockLevel.Should().Be(10);
            firstItem.Location.Should().Be("A1");
            firstItem.SupplierName.Should().Be("Supplier 1");
        }
    }
}


