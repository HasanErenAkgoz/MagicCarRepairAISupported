using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.PayTax;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Moq;
using Xunit;
using TaxEntity = MagicCarRepairAISupported.Domain.Entities.Tax;

namespace MagicCarRepairAISupported.Application.Tests.Features.Accounting.Tax.Commands.PayTax
{
    public class PayTaxCommandHandlerTests
    {
        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly PayTaxCommandHandler _handler;

        public PayTaxCommandHandlerTests()
        {
            _taxRepositoryMock = new Mock<ITaxRepository>();
            _mediatorMock = new Mock<IMediator>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TaxMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new PayTaxCommandHandler(
                _taxRepositoryMock.Object,
                _mediatorMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenTaxNotFound()
        {
            // Arrange
            var clientId = 1;
            var command = new PayTaxCommand
            {
                Id = 999,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Cash
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _taxRepositoryMock.Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((TaxEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenTaxNotBelongToClient()
        {
            // Arrange
            var clientId = 1;
            var otherClientId = 2;

            var tax = new TaxEntity
            {
                Id = 1,
                ClientId = otherClientId,
                TaxType = TaxType.VAT,
                Year = 2024,
                Amount = 1000,
                Status = TaxStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(30)
            };

            var command = new PayTaxCommand
            {
                Id = 1,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Cash
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _taxRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(tax);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenTaxAlreadyPaid()
        {
            // Arrange
            var clientId = 1;

            var tax = new TaxEntity
            {
                Id = 1,
                ClientId = clientId,
                TaxType = TaxType.VAT,
                Year = 2024,
                Amount = 1000,
                Status = TaxStatus.Paid,
                DueDate = DateTime.UtcNow.AddDays(30),
                PaymentDate = DateTime.UtcNow.AddDays(-10),
                PaymentMethod = PaymentMethod.Cash
            };

            var command = new PayTaxCommand
            {
                Id = 1,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Cash
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _taxRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(tax);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldPayTax_WhenValidCommand()
        {
            // Arrange
            var clientId = 1;
            var paymentDate = DateTime.UtcNow;

            var tax = new TaxEntity
            {
                Id = 1,
                ClientId = clientId,
                TaxType = TaxType.VAT,
                Year = 2024,
                Month = 1,
                Amount = 1000,
                Status = TaxStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(30),
                TaxOffice = "Test Office"
            };

            var command = new PayTaxCommand
            {
                Id = 1,
                PaymentDate = paymentDate,
                PaymentMethod = PaymentMethod.BankTransfer,
                PaymentReferenceNumber = "REF-001"
            };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);
            _taxRepositoryMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(tax);
            _taxRepositoryMock.Setup(x => x.Update(It.IsAny<TaxEntity>()))
                .Returns((TaxEntity t) => t);
            _taxRepositoryMock.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateExpenseCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CreateExpenseResponse { Id = 1 });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(TaxStatus.Paid);
            result.PaymentMethodName.Should().Be(PaymentMethod.BankTransfer.ToString());

            _taxRepositoryMock.Verify(x => x.Update(It.Is<TaxEntity>(t => 
                t.Status == TaxStatus.Paid &&
                t.PaymentDate == paymentDate &&
                t.PaymentMethod == PaymentMethod.BankTransfer &&
                t.PaymentReferenceNumber == "REF-001")), Times.Once);
            _taxRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            _mediatorMock.Verify(x => x.Send(It.Is<CreateExpenseCommand>(c => 
                c.ExpenseType == ExpenseType.Tax && 
                c.Amount == 1000 &&
                c.PaymentMethod == PaymentMethod.BankTransfer), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
