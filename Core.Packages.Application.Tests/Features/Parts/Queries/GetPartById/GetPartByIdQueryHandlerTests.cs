using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.Parts.Profiles;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetPartById;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Parts.Queries.GetPartById
{
    public class GetPartByIdQueryHandlerTests
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetPartByIdQueryHandler _handler;

        public GetPartByIdQueryHandlerTests()
        {
            _partRepositoryMock = new Mock<IPartRepository>();

            var mapperConfig = TestSupport.AutoMapperConfigurationFactory.Create(cfg =>
            {
                cfg.AddProfile<PartMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetPartByIdQueryHandler(_partRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPartNotFound()
        {
            // Arrange
            var query = new GetPartByIdQuery { Id = 1 };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnPart_WhenPartExists()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                Description = "Test Description",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original,
                Brand = "Test Brand",
                PurchasePrice = 100,
                SalePrice = 150,
                TaxRate = 20,
                MinimumStockLevel = 10,
                IsLowStockAlertEnabled = true,
                Unit = "Adet",
                ClientId = 1,
                Stock = new PartStock
                {
                    Id = 1,
                    PartId = 1,
                    Quantity = 50,
                    Location = "A1"
                },
                Supplier = new PartSupplier
                {
                    Id = 1,
                    CompanyName = "Test Supplier",
                    ClientId = 1
                }
            };

            var query = new GetPartByIdQuery { Id = 1 };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.PartCode.Should().Be("PART-001");
            result.Name.Should().Be("Test Part");
            result.Description.Should().Be("Test Description");
            result.Category.Should().Be(PartCategory.Engine);
            result.BrandType.Should().Be(PartBrandType.Original);
            result.PurchasePrice.Should().Be(100);
            result.SalePrice.Should().Be(150);
            result.StockQuantity.Should().Be(50);
            result.SupplierName.Should().Be("Test Supplier");
        }
    }
}
