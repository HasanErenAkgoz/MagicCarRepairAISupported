using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Update;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.PartSuppliers.Commands.Update
{
    public class UpdatePartSupplierCommandHandlerTests
    {
        private readonly Mock<IPartSupplierRepository> _partSupplierRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly UpdatePartSupplierCommandHandler _handler;

        public UpdatePartSupplierCommandHandlerTests()
        {
            _partSupplierRepositoryMock = new Mock<IPartSupplierRepository>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = TestSupport.AutoMapperConfigurationFactory.Create(cfg =>
            {
                cfg.AddProfile<PartSupplierMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdatePartSupplierCommandHandler(
                _partSupplierRepositoryMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSupplierNotFound()
        {
            // Arrange
            var command = new UpdatePartSupplierCommand
            {
                Id = 1,
                CompanyName = "Updated Supplier"
            };

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartSupplier?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenCompanyNameExists()
        {
            // Arrange
            var existingSupplier = new PartSupplier
            {
                Id = 1,
                CompanyName = "Existing Supplier",
                ClientId = 1
            };

            var duplicateSupplier = new PartSupplier
            {
                Id = 2,
                CompanyName = "Duplicate Supplier",
                ClientId = 1
            };

            var command = new UpdatePartSupplierCommand
            {
                Id = 1,
                CompanyName = "Duplicate Supplier"
            };

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingSupplier);

            _partSupplierRepositoryMock.Setup(x => x.GetByCompanyNameAsync("Duplicate Supplier", It.IsAny<CancellationToken>()))
                .ReturnsAsync(duplicateSupplier);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldUpdateSupplier_WhenValidCommand()
        {
            // Arrange
            var supplier = new PartSupplier
            {
                Id = 1,
                CompanyName = "Old Supplier",
                ContactPerson = "Old Person",
                Phone = "555-0000",
                Email = "old@test.com",
                IsActive = true,
                Status = Status.Active,
                ClientId = 1
            };

            var command = new UpdatePartSupplierCommand
            {
                Id = 1,
                CompanyName = "Updated Supplier",
                ContactPerson = "New Person",
                Phone = "555-1234",
                Email = "new@test.com",
                Address = "New Address",
                City = "Istanbul",
                Country = "Turkey",
                IsActive = true
            };

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(supplier);
            _tenantServiceMock.Setup(x => x.GetRequiredClientId()).Returns(1);
            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(1);

            _partSupplierRepositoryMock.Setup(x => x.GetByCompanyNameAsync("Updated Supplier", It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartSupplier?)null);

            _partSupplierRepositoryMock.Setup(x => x.Update(It.IsAny<PartSupplier>()));
            _partSupplierRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.CompanyName.Should().Be("Updated Supplier");
            result.ContactPerson.Should().Be("New Person");
            result.Phone.Should().Be("555-1234");
            result.Email.Should().Be("new@test.com");

            _partSupplierRepositoryMock.Verify(x => x.Update(It.IsAny<PartSupplier>()), Times.Once);
            _partSupplierRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
