using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Moq;
using Xunit;
using SalaryPaymentEntity = MagicCarRepairAISupported.Domain.Entities.SalaryPayment;

namespace MagicCarRepairAISupported.Application.Tests.Features.Accounting.SalaryPayment.Commands.Create
{
    public class CreateSalaryPaymentCommandHandlerTests
    {
        private readonly Mock<ISalaryPaymentRepository> _salaryPaymentRepositoryMock;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly CreateSalaryPaymentCommandHandler _handler;

        public CreateSalaryPaymentCommandHandlerTests()
        {
            _salaryPaymentRepositoryMock = new Mock<ISalaryPaymentRepository>();
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mediatorMock = new Mock<IMediator>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SalaryPaymentMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateSalaryPaymentCommandHandler(
                _salaryPaymentRepositoryMock.Object,
                _employeeRepositoryMock.Object,
                _mediatorMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
        {
            // Arrange
            var clientId = 1;
            var command = new CreateSalaryPaymentCommand
            {
                EmployeeId = 999,
                Month = 1,
                Year = 2024,
                GrossSalary = 10000,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Cash
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Employee?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotBelongToClient()
        {
            // Arrange
            var clientId = 1;
            var otherClientId = 2;

            var employee = new Employee
            {
                Id = 1,
                ClientId = otherClientId,
                FirstName = "Test",
                LastName = "Employee"
            };

            var command = new CreateSalaryPaymentCommand
            {
                EmployeeId = 1,
                Month = 1,
                Year = 2024,
                GrossSalary = 10000,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Cash
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(employee);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPaymentAlreadyExists()
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

            var existingPayment = new SalaryPaymentEntity
            {
                Id = 1,
                EmployeeId = 1,
                Month = 1,
                Year = 2024,
                ClientId = clientId
            };

            var command = new CreateSalaryPaymentCommand
            {
                EmployeeId = 1,
                Month = 1,
                Year = 2024,
                GrossSalary = 10000,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Cash
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(employee);
            _salaryPaymentRepositoryMock.Setup(x => x.GetByEmployeeAndPeriodAsync(1, 2024, 1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingPayment);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInvalidMonth()
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

            var command = new CreateSalaryPaymentCommand
            {
                EmployeeId = 1,
                Month = 13, // Invalid month
                Year = 2024,
                GrossSalary = 10000,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Cash
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(employee);
            _salaryPaymentRepositoryMock.Setup(x => x.GetByEmployeeAndPeriodAsync(1, 2024, 13, It.IsAny<CancellationToken>()))
                .ReturnsAsync((SalaryPaymentEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCreateSalaryPayment_WhenValidCommand()
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

            var command = new CreateSalaryPaymentCommand
            {
                EmployeeId = 1,
                Month = 1,
                Year = 2024,
                GrossSalary = 10000,
                SocialSecurityDeduction = 1400,
                UnemploymentInsuranceDeduction = 100,
                IncomeTaxDeduction = 1500,
                StampTax = 50,
                OtherDeductions = 0,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Cash,
                Description = "Test payment"
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(employee);
            _salaryPaymentRepositoryMock.Setup(x => x.GetByEmployeeAndPeriodAsync(1, 2024, 1, It.IsAny<CancellationToken>()))
                .ReturnsAsync((SalaryPaymentEntity?)null);
            SalaryPaymentEntity? addedPayment = null;
            _salaryPaymentRepositoryMock.Setup(x => x.AddAsync(It.IsAny<SalaryPaymentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SalaryPaymentEntity payment, CancellationToken ct) => { addedPayment = payment; return payment; });
            _salaryPaymentRepositoryMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateExpenseCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CreateExpenseResponse { Id = 1 });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.EmployeeId.Should().Be(1);
            result.Month.Should().Be(1);
            result.Year.Should().Be(2024);
            result.GrossSalary.Should().Be(10000);
            result.NetSalary.Should().Be(6950); // 10000 - 1400 - 100 - 1500 - 50 - 0

            _salaryPaymentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<SalaryPaymentEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _salaryPaymentRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            _mediatorMock.Verify(x => x.Send(It.Is<CreateExpenseCommand>(c => 
                c.ExpenseType == ExpenseType.Salary && 
                c.Amount == 6950), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
