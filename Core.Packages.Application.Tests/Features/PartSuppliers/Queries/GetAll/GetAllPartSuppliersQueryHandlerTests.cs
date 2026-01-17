using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Profiles;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetAll;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.PartSuppliers.Queries.GetAll
{
    public class GetAllPartSuppliersQueryHandlerTests
    {
        private readonly Mock<IPartSupplierRepository> _partSupplierRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetAllPartSuppliersQueryHandler _handler;

        public GetAllPartSuppliersQueryHandlerTests()
        {
            _partSupplierRepositoryMock = new Mock<IPartSupplierRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PartSupplierMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetAllPartSuppliersQueryHandler(_partSupplierRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoSuppliers()
        {
            // Arrange
            var query = new GetAllPartSuppliersQuery
            {
                PageNumber = 1,
                PageSize = 20
            };

            var suppliers = new List<PartSupplier>().AsQueryable();
            var mockSet = new Mock<DbSet<PartSupplier>>();
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.Provider).Returns(suppliers.Provider);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.Expression).Returns(suppliers.Expression);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.ElementType).Returns(suppliers.ElementType);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.GetEnumerator()).Returns(suppliers.GetEnumerator());

            _partSupplierRepositoryMock.Setup(x => x.Query())
                .Returns(mockSet.Object);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuppliers_WhenSuppliersExist()
        {
            // Arrange
            var query = new GetAllPartSuppliersQuery
            {
                PageNumber = 1,
                PageSize = 20
            };

            var suppliers = new List<PartSupplier>
            {
                new PartSupplier
                {
                    Id = 1,
                    CompanyName = "Supplier 1",
                    ContactPerson = "Person 1",
                    Phone = "555-0001",
                    Email = "supplier1@test.com",
                    IsActive = true,
                    Status = Status.Active,
                    ClientId = 1
                },
                new PartSupplier
                {
                    Id = 2,
                    CompanyName = "Supplier 2",
                    ContactPerson = "Person 2",
                    Phone = "555-0002",
                    Email = "supplier2@test.com",
                    IsActive = true,
                    Status = Status.Active,
                    ClientId = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<PartSupplier>>();
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.Provider).Returns(suppliers.Provider);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.Expression).Returns(suppliers.Expression);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.ElementType).Returns(suppliers.ElementType);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.GetEnumerator()).Returns(suppliers.GetEnumerator());

            _partSupplierRepositoryMock.Setup(x => x.Query())
                .Returns(mockSet.Object);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.TotalCount.Should().Be(2);
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(20);
        }

        [Fact]
        public async Task Handle_ShouldFilterActiveOnly_WhenIsActiveOnlyTrue()
        {
            // Arrange
            var query = new GetAllPartSuppliersQuery
            {
                PageNumber = 1,
                PageSize = 20,
                IsActiveOnly = true
            };

            var suppliers = new List<PartSupplier>
            {
                new PartSupplier
                {
                    Id = 1,
                    CompanyName = "Active Supplier",
                    IsActive = true,
                    Status = Status.Active,
                    ClientId = 1
                },
                new PartSupplier
                {
                    Id = 2,
                    CompanyName = "Inactive Supplier",
                    IsActive = false,
                    Status = Status.Active,
                    ClientId = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<PartSupplier>>();
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.Provider).Returns(suppliers.Provider);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.Expression).Returns(suppliers.Expression);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.ElementType).Returns(suppliers.ElementType);
            mockSet.As<IQueryable<PartSupplier>>().Setup(m => m.GetEnumerator()).Returns(suppliers.GetEnumerator());

            _partSupplierRepositoryMock.Setup(x => x.Query())
                .Returns(mockSet.Object);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().OnlyContain(s => s.IsActive == true);
        }
    }
}

