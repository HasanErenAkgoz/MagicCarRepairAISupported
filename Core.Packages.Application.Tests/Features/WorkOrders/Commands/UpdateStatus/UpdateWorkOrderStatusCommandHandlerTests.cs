using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateStatus;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.WorkOrders.Commands.UpdateStatus
{
    public class UpdateWorkOrderStatusCommandHandlerTests
    {
        private readonly Mock<IWorkOrderRepository> _workOrderRepositoryMock;
        private readonly Mock<IEntityRepository<Employee, int>> _employeeRepositoryMock;
        private readonly Mock<ISignalRNotificationService> _signalRNotificationServiceMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly UpdateWorkOrderStatusCommandHandler _handler;

        public UpdateWorkOrderStatusCommandHandlerTests()
        {
            _workOrderRepositoryMock = new Mock<IWorkOrderRepository>();
            _employeeRepositoryMock = new Mock<IEntityRepository<Employee, int>>();
            _signalRNotificationServiceMock = new Mock<ISignalRNotificationService>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WorkOrderMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdateWorkOrderStatusCommandHandler(
                _workOrderRepositoryMock.Object,
                _employeeRepositoryMock.Object,
                _signalRNotificationServiceMock.Object,
                _mapper,
                _tenantServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenWorkOrderNotFound()
        {
            // Arrange
            var clientId = 1;
            var command = new UpdateWorkOrderStatusCommand
            {
                WorkOrderId = 999,
                NewStatus = WorkOrderStatus.InProgress
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrder?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenWorkOrderNotBelongToClient()
        {
            // Arrange
            var clientId = 1;
            var otherClientId = 2;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = otherClientId,
                Status = WorkOrderStatus.VehicleEntered
            };

            var command = new UpdateWorkOrderStatusCommand
            {
                WorkOrderId = 1,
                NewStatus = WorkOrderStatus.InProgress
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenStatusAlreadySet()
        {
            // Arrange
            var clientId = 1;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                Status = WorkOrderStatus.InProgress,
                CustomerId = 1
            };

            var command = new UpdateWorkOrderStatusCommand
            {
                WorkOrderId = 1,
                NewStatus = WorkOrderStatus.InProgress
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
        {
            // Arrange
            var clientId = 1;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                Status = WorkOrderStatus.VehicleEntered,
                CustomerId = 1
            };

            var command = new UpdateWorkOrderStatusCommand
            {
                WorkOrderId = 1,
                NewStatus = WorkOrderStatus.InProgress,
                EmployeeId = 999
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Employee?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldUpdateStatus_WhenValidCommand()
        {
            // Arrange
            var clientId = 1;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                Status = WorkOrderStatus.VehicleEntered,
                CustomerId = 1
            };

            var command = new UpdateWorkOrderStatusCommand
            {
                WorkOrderId = 1,
                NewStatus = WorkOrderStatus.InProgress,
                Description = "Status updated"
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);
            _workOrderRepositoryMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);
            _workOrderRepositoryMock.Setup(x => x.AddTimelineAsync(It.IsAny<WorkOrderTimeline>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrderTimeline entity, CancellationToken ct) => entity);
            _signalRNotificationServiceMock.Setup(x => x.SendWorkOrderUpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.WorkOrderId.Should().Be(1);
            result.OldStatus.Should().Be(WorkOrderStatus.VehicleEntered);
            result.NewStatus.Should().Be(WorkOrderStatus.InProgress);

            _workOrderRepositoryMock.Verify(x => x.Update(It.IsAny<WorkOrder>()), Times.Once);
            _workOrderRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            _workOrderRepositoryMock.Verify(x => x.AddTimelineAsync(It.IsAny<WorkOrderTimeline>(), It.IsAny<CancellationToken>()), Times.Once);
            _signalRNotificationServiceMock.Verify(x => x.SendWorkOrderUpdateAsync(
                1,
                WorkOrderStatus.InProgress.ToString(),
                It.IsAny<string>(),
                1), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldUpdateStatus_WithEmployee()
        {
            // Arrange
            var clientId = 1;

            var employee = new Employee
            {
                Id = 1,
                ClientId = clientId,
                FirstName = "Test",
                LastName = "Employee"
            };

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                Status = WorkOrderStatus.VehicleEntered,
                CustomerId = 1
            };

            var command = new UpdateWorkOrderStatusCommand
            {
                WorkOrderId = 1,
                NewStatus = WorkOrderStatus.InProgress,
                EmployeeId = 1
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(employee);
            _workOrderRepositoryMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);
            _workOrderRepositoryMock.Setup(x => x.AddTimelineAsync(It.IsAny<WorkOrderTimeline>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrderTimeline entity, CancellationToken ct) => entity);
            _signalRNotificationServiceMock.Setup(x => x.SendWorkOrderUpdateAsync(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.NewStatus.Should().Be(WorkOrderStatus.InProgress);

            _workOrderRepositoryMock.Verify(x => x.AddTimelineAsync(
                It.Is<WorkOrderTimeline>(t => t.EmployeeId == 1),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

