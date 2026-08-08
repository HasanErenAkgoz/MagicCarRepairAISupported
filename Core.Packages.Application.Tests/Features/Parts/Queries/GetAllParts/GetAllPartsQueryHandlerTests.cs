using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetAllParts;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using Xunit;
using MagicCarRepairAISupported.Application.Tests.TestSupport;

namespace MagicCarRepairAISupported.Application.Tests.Features.Parts.Queries.GetAllParts
{
    public class GetAllPartsQueryHandlerTests
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly GetAllPartsQueryHandler _handler;

        public GetAllPartsQueryHandlerTests()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _handler = new GetAllPartsQueryHandler(_partRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoParts()
        {
            // Arrange
            var query = new GetAllPartsQuery
            {
                PageNumber = 1,
                PageSize = 20
            };

            var parts = new List<Part>().AsQueryable();
            var mockSet = new Mock<DbSet<Part>>();
            mockSet.As<IQueryable<Part>>().Setup(m => m.Provider).Returns(parts.Provider);
            mockSet.As<IQueryable<Part>>().Setup(m => m.Expression).Returns(parts.Expression);
            mockSet.As<IQueryable<Part>>().Setup(m => m.ElementType).Returns(parts.ElementType);
            mockSet.As<IQueryable<Part>>().Setup(m => m.GetEnumerator()).Returns(parts.GetEnumerator());

            _partRepositoryMock.Setup(x => x.Query())
                .Returns(parts.AsAsyncQueryable());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Parts.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
        }

        [Fact]
        public async Task Handle_ShouldReturnParts_WhenPartsExist()
        {
            // Arrange
            var query = new GetAllPartsQuery
            {
                PageNumber = 1,
                PageSize = 20
            };

            var parts = new List<Part>
            {
                new Part
                {
                    Id = 1,
                    PartCode = "PART-001",
                    Name = "Part 1",
                    Category = PartCategory.Engine,
                    BrandType = PartBrandType.Original,
                    ClientId = 1,
                    Stock = new PartStock { Quantity = 10 }
                },
                new Part
                {
                    Id = 2,
                    PartCode = "PART-002",
                    Name = "Part 2",
                    Category = PartCategory.BrakeSystem,
                    BrandType = PartBrandType.Aftermarket,
                    ClientId = 1,
                    Stock = new PartStock { Quantity = 5 }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Part>>();
            mockSet.As<IQueryable<Part>>().Setup(m => m.Provider).Returns(parts.Provider);
            mockSet.As<IQueryable<Part>>().Setup(m => m.Expression).Returns(parts.Expression);
            mockSet.As<IQueryable<Part>>().Setup(m => m.ElementType).Returns(parts.ElementType);
            mockSet.As<IQueryable<Part>>().Setup(m => m.GetEnumerator()).Returns(parts.GetEnumerator());

            _partRepositoryMock.Setup(x => x.Query())
                .Returns(parts.AsAsyncQueryable());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Parts.Should().HaveCount(2);
            result.TotalCount.Should().Be(2);
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(20);
        }

        [Fact]
        public async Task Handle_ShouldFilterByCategory_WhenCategoryProvided()
        {
            // Arrange
            var query = new GetAllPartsQuery
            {
                PageNumber = 1,
                PageSize = 20,
                Category = PartCategory.Engine
            };

            var parts = new List<Part>
            {
                new Part
                {
                    Id = 1,
                    PartCode = "PART-001",
                    Name = "Engine Part",
                    Category = PartCategory.Engine,
                    BrandType = PartBrandType.Original,
                    ClientId = 1
                },
                new Part
                {
                    Id = 2,
                    PartCode = "PART-002",
                    Name = "Brake Part",
                    Category = PartCategory.BrakeSystem,
                    BrandType = PartBrandType.Aftermarket,
                    ClientId = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Part>>();
            mockSet.As<IQueryable<Part>>().Setup(m => m.Provider).Returns(parts.Provider);
            mockSet.As<IQueryable<Part>>().Setup(m => m.Expression).Returns(parts.Expression);
            mockSet.As<IQueryable<Part>>().Setup(m => m.ElementType).Returns(parts.ElementType);
            mockSet.As<IQueryable<Part>>().Setup(m => m.GetEnumerator()).Returns(parts.GetEnumerator());

            _partRepositoryMock.Setup(x => x.Query())
                .Returns(parts.AsAsyncQueryable());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Parts.Should().OnlyContain(p => p.Category == PartCategory.Engine);
        }
    }
}
