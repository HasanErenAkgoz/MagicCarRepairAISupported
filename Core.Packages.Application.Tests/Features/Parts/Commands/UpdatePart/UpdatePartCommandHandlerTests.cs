using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services.Cache;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePart;
using MagicCarRepairAISupported.Application.Features.Parts.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Parts.Commands.UpdatePart
{
    public class UpdatePartCommandHandlerTests
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly Mock<IPartSupplierRepository> _partSupplierRepositoryMock;
        private readonly Mock<ICacheInvalidationService> _cacheInvalidationServiceMock;
        private readonly IMapper _mapper;
        private readonly UpdatePartCommandHandler _handler;

        public UpdatePartCommandHandlerTests()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _partSupplierRepositoryMock = new Mock<IPartSupplierRepository>();
            _cacheInvalidationServiceMock = new Mock<ICacheInvalidationService>();

            var mapperConfig = TestSupport.AutoMapperConfigurationFactory.Create(cfg =>
            {
                cfg.AddProfile<PartMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdatePartCommandHandler(
                _partRepositoryMock.Object,
                _partSupplierRepositoryMock.Object,
                _mapper,
                _cacheInvalidationServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPartNotFound()
        {
            // Arrange
            var command = new UpdatePartCommand
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
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

            var duplicatePart = new Part
            {
                Id = 2,
                PartCode = "PART-002",
                Name = "Duplicate Part",
                ClientId = 1
            };

            var command = new UpdatePartCommand
            {
                Id = 1,
                PartCode = "PART-002", // Duplicate code
                Name = "Test Part",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingPart);

            _partRepositoryMock.Setup(x => x.GetByPartCodeAsync("PART-002", It.IsAny<CancellationToken>()))
                .ReturnsAsync(duplicatePart);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldUpdatePart_WhenValidCommand()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Old Name",
                Description = "Old Description",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original,
                PurchasePrice = 100,
                SalePrice = 150,
                TaxRate = 20,
                MinimumStockLevel = 10,
                IsLowStockAlertEnabled = true,
                Unit = "Adet",
                ClientId = 1,
                Stock = new PartStock { Quantity = 5, Location = "A1" }
            };

            var command = new UpdatePartCommand
            {
                Id = 1,
                PartCode = "PART-001-UPDATED",
                Name = "New Name",
                Description = "New Description",
                Category = PartCategory.BrakeSystem,
                BrandType = PartBrandType.Aftermarket,
                PurchasePrice = 120,
                SalePrice = 180,
                TaxRate = 20,
                MinimumStockLevel = 15,
                IsLowStockAlertEnabled = true,
                Unit = "Adet"
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);
            _partRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);

            _partRepositoryMock.Setup(x => x.GetByPartCodeAsync("PART-001-UPDATED", It.IsAny<CancellationToken>()))
                .ReturnsAsync((Part?)null);

            _partRepositoryMock.Setup(x => x.Update(It.IsAny<Part>()));
            _partRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.PartCode.Should().Be("PART-001-UPDATED");
            result.Name.Should().Be("New Name");
            result.Description.Should().Be("New Description");
            result.Category.Should().Be(PartCategory.BrakeSystem);
            result.BrandType.Should().Be(PartBrandType.Aftermarket);
            result.PurchasePrice.Should().Be(120);
            result.SalePrice.Should().Be(180);
            result.MinimumStockLevel.Should().Be(15);

            _partRepositoryMock.Verify(x => x.Update(It.IsAny<Part>()), Times.Once);
            _partRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSupplierNotFound()
        {
            // Arrange
            var part = new Part
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                ClientId = 1
            };

            var command = new UpdatePartCommand
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                SupplierId = 999, // Non-existent supplier
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original
            };

            _partRepositoryMock.Setup(x => x.GetWithStockAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(part);

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartSupplier?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}

