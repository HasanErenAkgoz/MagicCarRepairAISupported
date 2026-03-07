using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Create;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.WorkOrders.Commands.Create
{
    public class CreateWorkOrderCommandHandlerTests
    {
        private readonly Mock<IWorkOrderRepository> _workOrderRepositoryMock;
        private readonly Mock<IEntityRepository<Vehicle, int>> _vehicleRepositoryMock;
        private readonly Mock<IEntityRepository<Customer, int>> _customerRepositoryMock;
        private readonly Mock<IEntityRepository<Employee, int>> _employeeRepositoryMock;
        private readonly Mock<IInsurancePolicyRepository> _insurancePolicyRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly Mock<ILogger<CreateWorkOrderCommandHandler>> _loggerMock;
        private readonly Mock<ISignalRNotificationService> _signalRNotificationServiceMock;
        private readonly IMapper _mapper;
        private readonly CreateWorkOrderCommandHandler _handler;

        public CreateWorkOrderCommandHandlerTests()
        {
            _workOrderRepositoryMock = new Mock<IWorkOrderRepository>();
            _vehicleRepositoryMock = new Mock<IEntityRepository<Vehicle, int>>();
            _customerRepositoryMock = new Mock<IEntityRepository<Customer, int>>();
            _employeeRepositoryMock = new Mock<IEntityRepository<Employee, int>>();
            _insurancePolicyRepositoryMock = new Mock<IInsurancePolicyRepository>();
            _tenantServiceMock = new Mock<ITenantService>();
            _loggerMock = new Mock<ILogger<CreateWorkOrderCommandHandler>>();
            _signalRNotificationServiceMock = new Mock<ISignalRNotificationService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WorkOrderMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateWorkOrderCommandHandler(
                _workOrderRepositoryMock.Object,
                _vehicleRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _employeeRepositoryMock.Object,
                _insurancePolicyRepositoryMock.Object,
                _mapper,
                _tenantServiceMock.Object,
                _loggerMock.Object,
                _signalRNotificationServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenVehicleNotFound()
        {
            // Arrange
            var clientId = 1;
            var command = new CreateWorkOrderCommand
            {
                VehicleId = 999,
                CustomerId = 1
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Vehicle?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenCustomerNotFound()
        {
            // Arrange
            var clientId = 1;
            var vehicle = new Vehicle
            {
                Id = 1,
                ClientId = clientId,
                CustomerId = 1,
                LicensePlate = "34ABC123"
            };

            var command = new CreateWorkOrderCommand
            {
                VehicleId = 1,
                CustomerId = 999
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Customer?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenVehicleNotBelongToCustomer()
        {
            // Arrange
            var clientId = 1;
            var vehicle = new Vehicle
            {
                Id = 1,
                ClientId = clientId,
                CustomerId = 1,
                LicensePlate = "34ABC123"
            };

            var customer = new Customer
            {
                Id = 2,
                ClientId = clientId,
                FirstName = "Test",
                LastName = "Customer"
            };

            var command = new CreateWorkOrderCommand
            {
                VehicleId = 1,
                CustomerId = 2
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(2, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
        {
            // Arrange
            var clientId = 1;
            var vehicle = new Vehicle
            {
                Id = 1,
                ClientId = clientId,
                CustomerId = 1,
                LicensePlate = "34ABC123"
            };

            var customer = new Customer
            {
                Id = 1,
                ClientId = clientId,
                FirstName = "Test",
                LastName = "Customer"
            };

            var command = new CreateWorkOrderCommand
            {
                VehicleId = 1,
                CustomerId = 1,
                AssignedEmployeeId = 999
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Employee?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCreateWorkOrder_WhenValidCommand()
        {
            // Arrange
            var clientId = 1;
            var vehicle = new Vehicle
            {
                Id = 1,
                ClientId = clientId,
                CustomerId = 1,
                LicensePlate = "34ABC123"
            };

            var customer = new Customer
            {
                Id = 1,
                ClientId = clientId,
                FirstName = "Test",
                LastName = "Customer"
            };

            var command = new CreateWorkOrderCommand
            {
                VehicleId = 1,
                CustomerId = 1,
                CustomerComplaints = "Test complaint",
                Priority = WorkOrderPriority.Normal,
                Kilometers = 50000,
                FuelLevel = 50
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);
            _insurancePolicyRepositoryMock.Setup(x => x.GetActivePolicyForVehicleAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((InsurancePolicy?)null);

            _workOrderRepositoryMock.Setup(x => x.AddAsync(It.IsAny<WorkOrder>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrder entity, CancellationToken ct) => 
                {
                    entity.Id = 1;
                    entity.CreatedDate = DateTime.UtcNow;
                    return entity;
                });

            _workOrderRepositoryMock.Setup(x => x.AddTimelineAsync(It.IsAny<WorkOrderTimeline>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrderTimeline entity, CancellationToken ct) => entity);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.VehicleId.Should().Be(1);
            result.CustomerId.Should().Be(1);
            result.Status.Should().Be(WorkOrderStatus.VehicleEntered);
            result.Priority.Should().Be(WorkOrderPriority.Normal);

            _workOrderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<WorkOrder>(), It.IsAny<CancellationToken>()), Times.Once);
            _workOrderRepositoryMock.Verify(x => x.AddTimelineAsync(It.IsAny<WorkOrderTimeline>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCreateWorkOrder_WhenActiveInsurancePolicyExists()
        {
            // Arrange
            var clientId = 1;
            var vehicle = new Vehicle
            {
                Id = 1,
                ClientId = clientId,
                CustomerId = 1,
                LicensePlate = "34ABC123"
            };

            var customer = new Customer
            {
                Id = 1,
                ClientId = clientId,
                FirstName = "Test",
                LastName = "Customer"
            };

            var insuranceCompany = new InsuranceCompany
            {
                Id = 1,
                CompanyName = "Test Insurance",
                CompanyCode = "TEST"
            };

            var insurancePolicy = new InsurancePolicy
            {
                Id = 1,
                PolicyNumber = "POL-001",
                VehicleId = 1,
                CustomerId = 1,
                InsuranceCompanyId = 1,
                InsuranceCompany = insuranceCompany,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow.AddDays(335),
                Status = InsuranceStatus.Active
            };

            var command = new CreateWorkOrderCommand
            {
                VehicleId = 1,
                CustomerId = 1
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);
            _insurancePolicyRepositoryMock.Setup(x => x.GetActivePolicyForVehicleAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(insurancePolicy);

            _workOrderRepositoryMock.Setup(x => x.AddAsync(It.IsAny<WorkOrder>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrder entity, CancellationToken ct) => 
                {
                    entity.Id = 1;
                    entity.CreatedDate = DateTime.UtcNow;
                    return entity;
                });

            _workOrderRepositoryMock.Setup(x => x.AddTimelineAsync(It.IsAny<WorkOrderTimeline>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrderTimeline entity, CancellationToken ct) => entity);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            _insurancePolicyRepositoryMock.Verify(x => x.GetActivePolicyForVehicleAsync(1, It.IsAny<CancellationToken>()), Times.Once);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Active insurance policy")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}

