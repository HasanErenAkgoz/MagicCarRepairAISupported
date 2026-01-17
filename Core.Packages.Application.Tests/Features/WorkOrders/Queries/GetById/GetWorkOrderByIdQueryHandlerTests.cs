using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Profiles;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.WorkOrders.Queries.GetById
{
    public class GetWorkOrderByIdQueryHandlerTests
    {
        private readonly Mock<IWorkOrderRepository> _workOrderRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly GetWorkOrderByIdQueryHandler _handler;

        public GetWorkOrderByIdQueryHandlerTests()
        {
            _workOrderRepositoryMock = new Mock<IWorkOrderRepository>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WorkOrderMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetWorkOrderByIdQueryHandler(
                _workOrderRepositoryMock.Object,
                _mapper,
                _tenantServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenWorkOrderNotFound()
        {
            // Arrange
            var clientId = 1;
            var query = new GetWorkOrderByIdQuery { Id = 999 };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetWithDetailsAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkOrder?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
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
                WorkOrderNumber = "WO-001",
                Status = WorkOrderStatus.VehicleEntered
            };

            var query = new GetWorkOrderByIdQuery { Id = 1 };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetWithDetailsAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnWorkOrder_WhenValidQuery()
        {
            // Arrange
            var clientId = 1;

            var vehicle = new Vehicle
            {
                Id = 1,
                LicensePlate = "34ABC123",
                Brand = "Toyota",
                Model = "Corolla"
            };

            var customer = new Customer
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Customer"
            };

            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Employee"
            };

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                WorkOrderNumber = "WO-001",
                VehicleId = 1,
                Vehicle = vehicle,
                CustomerId = 1,
                Customer = customer,
                AssignedEmployeeId = 1,
                AssignedEmployee = employee,
                Status = WorkOrderStatus.VehicleEntered,
                Priority = WorkOrderPriority.Normal,
                PaymentStatus = PaymentStatus.Unpaid,
                EntryDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            var query = new GetWorkOrderByIdQuery { Id = 1 };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetWithDetailsAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.WorkOrderNumber.Should().Be("WO-001");
            result.VehicleLicensePlate.Should().Be("34ABC123");
            result.VehicleBrand.Should().Be("Toyota");
            result.VehicleModel.Should().Be("Corolla");
            result.CustomerName.Should().Be("Test Customer");
            result.AssignedEmployeeName.Should().Be("Test Employee");
            result.StatusName.Should().Be(WorkOrderStatus.VehicleEntered.ToString());
            result.PriorityName.Should().Be(WorkOrderPriority.Normal.ToString());
            result.PaymentStatusName.Should().Be(PaymentStatus.Unpaid.ToString());
        }

        [Fact]
        public async Task Handle_ShouldReturnWorkOrder_WithNullNavigationProperties()
        {
            // Arrange
            var clientId = 1;

            var workOrder = new WorkOrder
            {
                Id = 1,
                ClientId = clientId,
                WorkOrderNumber = "WO-001",
                Status = WorkOrderStatus.VehicleEntered,
                Priority = WorkOrderPriority.Normal,
                PaymentStatus = PaymentStatus.Unpaid,
                EntryDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            var query = new GetWorkOrderByIdQuery { Id = 1 };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _workOrderRepositoryMock.Setup(x => x.GetWithDetailsAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workOrder);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.VehicleLicensePlate.Should().BeNullOrEmpty();
            result.CustomerName.Should().BeNullOrEmpty();
            result.AssignedEmployeeName.Should().BeNullOrEmpty();
        }
    }
}
