using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Create;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.PartSuppliers.Commands.Create
{
    public class CreatePartSupplierCommandHandlerTests
    {
        private readonly Mock<IPartSupplierRepository> _partSupplierRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly CreatePartSupplierCommandHandler _handler;

        public CreatePartSupplierCommandHandlerTests()
        {
            _partSupplierRepositoryMock = new Mock<IPartSupplierRepository>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = TestSupport.AutoMapperConfigurationFactory.Create(cfg =>
            {
                cfg.AddProfile<PartSupplierMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreatePartSupplierCommandHandler(
                _partSupplierRepositoryMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenClientIdNotFound()
        {
            // Arrange
            var command = new CreatePartSupplierCommand
            {
                CompanyName = "Test Supplier",
                IsActive = true
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId())
                .Returns((int?)null);

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

            var command = new CreatePartSupplierCommand
            {
                CompanyName = "Existing Supplier",
                IsActive = true
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId())
                .Returns(1);

            _partSupplierRepositoryMock.Setup(x => x.GetByCompanyNameAsync("Existing Supplier", It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingSupplier);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCreateSupplier_WhenValidCommand()
        {
            // Arrange
            var command = new CreatePartSupplierCommand
            {
                CompanyName = "New Supplier",
                ContactPerson = "John Doe",
                Phone = "555-1234",
                Email = "supplier@test.com",
                Address = "123 Main St",
                City = "Istanbul",
                Country = "Turkey",
                IsActive = true
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId())
                .Returns(1);

            _partSupplierRepositoryMock.Setup(x => x.GetByCompanyNameAsync("New Supplier", It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartSupplier?)null);

            _partSupplierRepositoryMock.Setup(x => x.AddAsync(It.IsAny<PartSupplier>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartSupplier entity, CancellationToken ct) => entity);

            _partSupplierRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.CompanyName.Should().Be("New Supplier");
            result.ContactPerson.Should().Be("John Doe");
            result.Phone.Should().Be("555-1234");
            result.Email.Should().Be("supplier@test.com");
            result.IsActive.Should().BeTrue();

            _partSupplierRepositoryMock.Verify(x => x.AddAsync(It.Is<PartSupplier>(s => 
                s.CompanyName == "New Supplier" &&
                s.ClientId == 1 &&
                s.Status == Status.Active), It.IsAny<CancellationToken>()), Times.Once);
            _partSupplierRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}

