using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services.Cache;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.CreatePart;
using MagicCarRepairAISupported.Application.Features.Parts.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Parts.Commands.CreatePart
{
    public class CreatePartCommandHandlerTests
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly Mock<IPartStockRepository> _partStockRepositoryMock;
        private readonly Mock<IPartSupplierRepository> _partSupplierRepositoryMock;
        private readonly Mock<ICacheInvalidationService> _cacheInvalidationServiceMock;
        private readonly IMapper _mapper;
        private readonly CreatePartCommandHandler _handler;

        public CreatePartCommandHandlerTests()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _partStockRepositoryMock = new Mock<IPartStockRepository>();
            _partSupplierRepositoryMock = new Mock<IPartSupplierRepository>();
            _cacheInvalidationServiceMock = new Mock<ICacheInvalidationService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PartMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreatePartCommandHandler(
                _partRepositoryMock.Object,
                _partStockRepositoryMock.Object,
                _partSupplierRepositoryMock.Object,
                _mapper,
                _cacheInvalidationServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPartCodeExists()
        {
            // Arrange
            var existingPart = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Existing Part",
                ClientId = 1
            };

            var command = new CreatePartCommand
            {
                PartCode = "PART-001",
                Name = "Test Part",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original
            };

            _partRepositoryMock.Setup(x => x.GetByPartCodeAsync("PART-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingPart);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSupplierNotFound()
        {
            // Arrange
            var command = new CreatePartCommand
            {
                PartCode = "PART-001",
                Name = "Test Part",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original,
                SupplierId = 999 // Non-existent supplier
            };

            _partRepositoryMock.Setup(x => x.GetByPartCodeAsync("PART-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartSupplier?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCreatePart_WhenValidCommand()
        {
            // Arrange
            var command = new CreatePartCommand
            {
                PartCode = "PART-001",
                Name = "New Part",
                Description = "Test Description",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original,
                Brand = "Test Brand",
                PurchasePrice = 100,
                SalePrice = 150,
                TaxRate = 20,
                MinimumStockLevel = 10,
                IsLowStockAlertEnabled = true,
                Unit = "Adet"
            };

            _partRepositoryMock.Setup(x => x.GetByPartCodeAsync("PART-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            _partRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Part>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part entity, CancellationToken ct) => entity);

            _partRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.PartCode.Should().Be("PART-001");
            result.Name.Should().Be("New Part");
            result.Description.Should().Be("Test Description");
            result.Category.Should().Be(PartCategory.Engine);

            _partRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Part>(), It.IsAny<CancellationToken>()), Times.Once);
            _partRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCreatePartWithStock_WhenInitialStockProvided()
        {
            // Arrange
            var command = new CreatePartCommand
            {
                PartCode = "PART-001",
                Name = "New Part",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original,
                InitialStockQuantity = 50,
                StockLocation = "A1"
            };

            _partRepositoryMock.Setup(x => x.GetByPartCodeAsync("PART-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            _partRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Part>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part entity, CancellationToken ct) => entity);

            _partRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            _partStockRepositoryMock.Setup(x => x.AddAsync(It.IsAny<PartStock>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartStock entity, CancellationToken ct) => entity);

            _partStockRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StockQuantity.Should().Be(50);

            _partRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Part>(), It.IsAny<CancellationToken>()), Times.Once);
            _partStockRepositoryMock.Verify(x => x.AddAsync(It.Is<PartStock>(s => 
                s.Quantity == 50 && s.Location == "A1"), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}


