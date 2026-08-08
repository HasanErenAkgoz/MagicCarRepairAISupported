using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Profiles;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.PartSuppliers.Queries.GetById
{
    public class GetPartSupplierByIdQueryHandlerTests
    {
        private readonly Mock<IPartSupplierRepository> _partSupplierRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetPartSupplierByIdQueryHandler _handler;

        public GetPartSupplierByIdQueryHandlerTests()
        {
            _partSupplierRepositoryMock = new Mock<IPartSupplierRepository>();

            var mapperConfig = TestSupport.AutoMapperConfigurationFactory.Create(cfg =>
            {
                cfg.AddProfile<PartSupplierMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetPartSupplierByIdQueryHandler(_partSupplierRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSupplierNotFound()
        {
            // Arrange
            var query = new GetPartSupplierByIdQuery { Id = 1 };

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((PartSupplier?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSupplierDeleted()
        {
            // Arrange
            var supplier = new PartSupplier
            {
                Id = 1,
                CompanyName = "Deleted Supplier",
                Status = Status.Deleted,
                ClientId = 1
            };

            var query = new GetPartSupplierByIdQuery { Id = 1 };

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(supplier);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnSupplier_WhenSupplierExists()
        {
            // Arrange
            var supplier = new PartSupplier
            {
                Id = 1,
                CompanyName = "Test Supplier",
                ContactPerson = "John Doe",
                Phone = "555-1234",
                Email = "supplier@test.com",
                Address = "123 Main St",
                City = "Istanbul",
                Country = "Turkey",
                IsActive = true,
                Status = Status.Active,
                ClientId = 1
            };

            var query = new GetPartSupplierByIdQuery { Id = 1 };

            _partSupplierRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(supplier);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.CompanyName.Should().Be("Test Supplier");
            result.ContactPerson.Should().Be("John Doe");
            result.Phone.Should().Be("555-1234");
            result.Email.Should().Be("supplier@test.com");
            result.Address.Should().Be("123 Main St");
            result.City.Should().Be("Istanbul");
            result.IsActive.Should().BeTrue();
        }
    }
}

