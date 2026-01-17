using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.CreatePart;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePart;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetAllParts;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetPartById;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.WebAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.WebAPI.Tests.Controllers
{
    public class PartsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly PartsController _controller;

        public PartsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new PartsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult()
        {
            // Arrange
            var response = new GetAllPartsResponse
            {
                Parts = new List<GetAllPartsResponse.PartDto>
                {
                    new GetAllPartsResponse.PartDto
                    {
                        Id = 1,
                        PartCode = "PART-001",
                        Name = "Test Part",
                        Category = PartCategory.Engine
                    }
                },
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 20
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllPartsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult()
        {
            // Arrange
            var response = new GetPartByIdResponse
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Test Part",
                Category = PartCategory.Engine
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetPartByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var command = new CreatePartCommand
            {
                PartCode = "PART-001",
                Name = "New Part",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original
            };

            var response = new CreatePartResponse
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "New Part"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreatePartCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Create(command);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtResult = result as CreatedAtActionResult;
            createdAtResult!.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
        {
            // Arrange
            var command = new UpdatePartCommand
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Updated Part"
            };

            // Act
            var result = await _controller.Update(2, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_ShouldReturnOkResult_WhenValid()
        {
            // Arrange
            var command = new UpdatePartCommand
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Updated Part",
                Category = PartCategory.Engine,
                BrandType = PartBrandType.Original
            };

            var response = new UpdatePartResponse
            {
                Id = 1,
                PartCode = "PART-001",
                Name = "Updated Part"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdatePartCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Update(1, command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(response);
        }
    }
}

