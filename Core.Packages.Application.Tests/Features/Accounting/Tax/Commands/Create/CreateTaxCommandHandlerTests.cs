using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;
using TaxEntity = MagicCarRepairAISupported.Domain.Entities.Tax;

namespace MagicCarRepairAISupported.Application.Tests.Features.Accounting.Tax.Commands.Create
{
    public class CreateTaxCommandHandlerTests
    {
        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly CreateTaxCommandHandler _handler;

        public CreateTaxCommandHandlerTests()
        {
            _taxRepositoryMock = new Mock<ITaxRepository>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TaxMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateTaxCommandHandler(
                _taxRepositoryMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInvalidYear()
        {
            // Arrange
            var clientId = 1;
            var command = new CreateTaxCommand
            {
                TaxType = TaxType.VAT,
                Year = 1999, // Invalid year
                Amount = 1000,
                DueDate = DateTime.UtcNow.AddDays(30)
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInvalidMonth()
        {
            // Arrange
            var clientId = 1;
            var command = new CreateTaxCommand
            {
                TaxType = TaxType.VAT,
                Month = 13, // Invalid month
                Year = 2024,
                Amount = 1000,
                DueDate = DateTime.UtcNow.AddDays(30)
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInvalidAmount()
        {
            // Arrange
            var clientId = 1;
            var command = new CreateTaxCommand
            {
                TaxType = TaxType.VAT,
                Year = 2024,
                Amount = 0, // Invalid amount
                DueDate = DateTime.UtcNow.AddDays(30)
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCreateTax_WhenValidCommand()
        {
            // Arrange
            var clientId = 1;
            var command = new CreateTaxCommand
            {
                TaxType = TaxType.VAT,
                Month = 1,
                Year = 2024,
                Amount = 1000,
                DueDate = DateTime.UtcNow.AddDays(30),
                Description = "Test tax",
                TaxOffice = "Test Office",
                TaxNumber = "TAX-001"
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            TaxEntity? addedTax = null;
            _taxRepositoryMock.Setup(x => x.AddAsync(It.IsAny<TaxEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((TaxEntity tax, CancellationToken ct) => { addedTax = tax; return tax; });
            _taxRepositoryMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.TaxType.Should().Be(TaxType.VAT);
            result.Year.Should().Be(2024);
            result.Month.Should().Be(1);
            result.Amount.Should().Be(1000);
            result.Status.Should().Be(TaxStatus.Pending);

            _taxRepositoryMock.Verify(x => x.AddAsync(It.Is<TaxEntity>(t => 
                t.TaxType == TaxType.VAT &&
                t.Year == 2024 &&
                t.Month == 1 &&
                t.Amount == 1000 &&
                t.Status == TaxStatus.Pending &&
                t.ClientId == clientId), It.IsAny<CancellationToken>()), Times.Once);
            _taxRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCreateTax_WhenMonthIsNull()
        {
            // Arrange
            var clientId = 1;
            var command = new CreateTaxCommand
            {
                TaxType = TaxType.CorporateTax,
                Month = null, // Annual tax
                Year = 2024,
                Amount = 5000,
                DueDate = DateTime.UtcNow.AddDays(60)
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            TaxEntity? addedTax = null;
            _taxRepositoryMock.Setup(x => x.AddAsync(It.IsAny<TaxEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((TaxEntity tax, CancellationToken ct) => { addedTax = tax; return tax; });
            _taxRepositoryMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.TaxType.Should().Be(TaxType.CorporateTax);
            result.Year.Should().Be(2024);
            result.Month.Should().BeNull();

            _taxRepositoryMock.Verify(x => x.AddAsync(It.Is<TaxEntity>(t => 
                t.TaxType == TaxType.CorporateTax &&
                t.Year == 2024 &&
                t.Month == null), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
