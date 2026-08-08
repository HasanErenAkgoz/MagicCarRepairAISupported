using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsurancePolicy;
using MagicCarRepairAISupported.Application.Features.Insurance.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Moq;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Features.Insurance.Commands.CreateInsurancePolicy
{
    public class CreateInsurancePolicyCommandHandlerTests
    {
        private readonly Mock<IInsurancePolicyRepository> _insurancePolicyRepositoryMock;
        private readonly Mock<IInsuranceCompanyRepository> _insuranceCompanyRepositoryMock;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly CreateInsurancePolicyCommandHandler _handler;

        public CreateInsurancePolicyCommandHandlerTests()
        {
            _insurancePolicyRepositoryMock = new Mock<IInsurancePolicyRepository>();
            _insuranceCompanyRepositoryMock = new Mock<IInsuranceCompanyRepository>();
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = TestSupport.AutoMapperConfigurationFactory.Create(cfg =>
            {
                cfg.AddProfile<InsuranceMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateInsurancePolicyCommandHandler(
                _insurancePolicyRepositoryMock.Object,
                _insuranceCompanyRepositoryMock.Object,
                _vehicleRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPolicyNumberExists()
        {
            // Arrange
            var clientId = 1;

            var existingPolicy = new InsurancePolicy
            {
                Id = 1,
                PolicyNumber = "POL-001"
            };

            var command = new CreateInsurancePolicyCommand
            {
                PolicyNumber = "POL-001",
                VehicleId = 1,
                CustomerId = 1,
                InsuranceCompanyId = 1
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _insurancePolicyRepositoryMock.Setup(x => x.GetByPolicyNumberAsync("POL-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingPolicy);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenVehicleNotFound()
        {
            // Arrange
            var clientId = 1;

            var command = new CreateInsurancePolicyCommand
            {
                PolicyNumber = "POL-001",
                VehicleId = 999,
                CustomerId = 1,
                InsuranceCompanyId = 1
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _insurancePolicyRepositoryMock.Setup(x => x.GetByPolicyNumberAsync("POL-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync((InsurancePolicy?)null);
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
                ClientId = clientId
            };

            var command = new CreateInsurancePolicyCommand
            {
                PolicyNumber = "POL-001",
                VehicleId = 1,
                CustomerId = 999,
                InsuranceCompanyId = 1
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _insurancePolicyRepositoryMock.Setup(x => x.GetByPolicyNumberAsync("POL-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync((InsurancePolicy?)null);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Customer?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInsuranceCompanyNotFoundOrInactive()
        {
            // Arrange
            var clientId = 1;

            var vehicle = new Vehicle
            {
                Id = 1,
                ClientId = clientId
            };

            var customer = new Customer
            {
                Id = 1,
                ClientId = clientId,
                FirstName = "Test",
                LastName = "Customer"
            };

            var command = new CreateInsurancePolicyCommand
            {
                PolicyNumber = "POL-001",
                VehicleId = 1,
                CustomerId = 1,
                InsuranceCompanyId = 999
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _insurancePolicyRepositoryMock.Setup(x => x.GetByPolicyNumberAsync("POL-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync((InsurancePolicy?)null);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);
            _insuranceCompanyRepositoryMock.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((InsuranceCompany?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEndDateBeforeStartDate()
        {
            // Arrange
            var clientId = 1;

            var vehicle = new Vehicle
            {
                Id = 1,
                ClientId = clientId
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
                ClientId = clientId,
                IsActive = true
            };

            var command = new CreateInsurancePolicyCommand
            {
                PolicyNumber = "POL-001",
                VehicleId = 1,
                CustomerId = 1,
                InsuranceCompanyId = 1,
                StartDate = DateTime.UtcNow.AddDays(30),
                EndDate = DateTime.UtcNow // End date before start date
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _insurancePolicyRepositoryMock.Setup(x => x.GetByPolicyNumberAsync("POL-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync((InsurancePolicy?)null);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);
            _insuranceCompanyRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(insuranceCompany);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCreatePolicy_WhenValidCommand()
        {
            // Arrange
            var clientId = 1;

            var vehicle = new Vehicle
            {
                Id = 1,
                ClientId = clientId
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
                ClientId = clientId,
                IsActive = true,
                CompanyName = "Test Insurance"
            };

            var command = new CreateInsurancePolicyCommand
            {
                PolicyNumber = "POL-001",
                VehicleId = 1,
                CustomerId = 1,
                InsuranceCompanyId = 1,
                InsuranceType = InsuranceType.Comprehensive,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddYears(1),
                PremiumAmount = 5000,
                CoverageAmount = 200000,
                DeductiblePercentage = 10
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _insurancePolicyRepositoryMock.Setup(x => x.GetByPolicyNumberAsync("POL-001", It.IsAny<CancellationToken>()))
                .ReturnsAsync((InsurancePolicy?)null);
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(vehicle);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);
            _insuranceCompanyRepositoryMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(insuranceCompany);
            _insurancePolicyRepositoryMock.Setup(x => x.AddAsync(It.IsAny<InsurancePolicy>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((InsurancePolicy entity, CancellationToken ct) =>
                {
                    entity.Id = 1;
                    return entity;
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.PolicyNumber.Should().Be("POL-001");
            result.InsuranceType.Should().Be(InsuranceType.Comprehensive);

            _insurancePolicyRepositoryMock.Verify(x => x.AddAsync(
                It.Is<InsurancePolicy>(p =>
                    p.PolicyNumber == "POL-001" &&
                    p.VehicleId == 1 &&
                    p.CustomerId == 1 &&
                    p.InsuranceCompanyId == 1 &&
                    p.Status == InsuranceStatus.Active &&
                    p.ClientId == clientId),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
