using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Invoices.Profiles;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using Xunit;
using MagicCarRepairAISupported.Application.Tests.TestSupport;

namespace MagicCarRepairAISupported.Application.Tests.Features.Invoices.Queries.GetById
{
    public class GetInvoiceByIdQueryHandlerTests
    {
        private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock;
        private readonly Mock<ITenantService> _tenantServiceMock;
        private readonly IMapper _mapper;
        private readonly GetInvoiceByIdQueryHandler _handler;

        public GetInvoiceByIdQueryHandlerTests()
        {
            _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            _tenantServiceMock = new Mock<ITenantService>();

            var mapperConfig = TestSupport.AutoMapperConfigurationFactory.Create(cfg =>
            {
                cfg.AddProfile<InvoiceMappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetInvoiceByIdQueryHandler(
                _invoiceRepositoryMock.Object,
                _tenantServiceMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInvoiceNotFound()
        {
            // Arrange
            var clientId = 1;
            var query = new GetInvoiceByIdQuery { Id = 999 };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);

            var mockQueryable = new List<Invoice>().AsQueryable();
            _invoiceRepositoryMock.Setup(x => x.Query())
                .Returns(mockQueryable.AsAsyncQueryable());

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenInvoiceNotBelongToClient()
        {
            // Arrange
            var clientId = 1;
            var otherClientId = 2;

            var invoice = new Invoice
            {
                Id = 1,
                ClientId = otherClientId,
                InvoiceNumber = "INV-001",
                Status = InvoiceStatus.Pending
            };

            var query = new GetInvoiceByIdQuery { Id = 1 };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);

            var mockQueryable = new List<Invoice> { invoice }.AsQueryable();
            _invoiceRepositoryMock.Setup(x => x.Query())
                .Returns(mockQueryable.AsAsyncQueryable());

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnInvoice_WhenValidQuery()
        {
            // Arrange
            var clientId = 1;

            var customer = new Customer
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Customer"
            };

            var workOrder = new WorkOrder
            {
                Id = 1,
                WorkOrderNumber = "WO-001"
            };

            var invoiceItem = new InvoiceItem
            {
                Id = 1,
                Description = "Test Item",
                Quantity = 2,
                UnitPrice = 100,
                TaxRate = 20
            };
            invoiceItem.CalculateTotal(); // Calculate read-only properties

            var invoice = new Invoice
            {
                Id = 1,
                ClientId = clientId,
                InvoiceNumber = "INV-001",
                InvoiceType = InvoiceType.Sales,
                WorkOrderId = 1,
                WorkOrder = workOrder,
                CustomerId = 1,
                Customer = customer,
                InvoiceDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                SubTotal = 200,
                TaxAmount = 40,
                TotalAmount = 240,
                PaidAmount = 0,
                Status = InvoiceStatus.Pending,
                Description = "Test Invoice",
                Items = new List<InvoiceItem> { invoiceItem },
                CreatedDate = DateTime.UtcNow
            };

            var query = new GetInvoiceByIdQuery { Id = 1 };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);

            var mockQueryable = new List<Invoice> { invoice }.AsQueryable();
            _invoiceRepositoryMock.Setup(x => x.Query())
                .Returns(mockQueryable.AsAsyncQueryable());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.InvoiceNumber.Should().Be("INV-001");
            result.InvoiceTypeName.Should().Be(InvoiceType.Sales.ToString());
            result.StatusName.Should().Be(InvoiceStatus.Pending.ToString());
            result.WorkOrderNumber.Should().Be("WO-001");
            result.CustomerName.Should().Be("Test Customer");
            result.RemainingAmount.Should().Be(240);
            result.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task Handle_ShouldReturnInvoice_WithNullNavigationProperties()
        {
            // Arrange
            var clientId = 1;

            var invoice = new Invoice
            {
                Id = 1,
                ClientId = clientId,
                InvoiceNumber = "INV-001",
                InvoiceType = InvoiceType.Sales,
                Status = InvoiceStatus.Pending,
                InvoiceDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            var query = new GetInvoiceByIdQuery { Id = 1 };

            _tenantServiceMock.Setup(x => x.GetCurrentClientId()).Returns(clientId);

            var mockQueryable = new List<Invoice> { invoice }.AsQueryable();
            _invoiceRepositoryMock.Setup(x => x.Query())
                .Returns(mockQueryable.AsAsyncQueryable());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.WorkOrderNumber.Should().BeNull();
            result.CustomerName.Should().BeNull();
            result.SupplierName.Should().BeNull();
        }
    }
}
