using AutoMapper;
using FluentAssertions;
using MagicCarRepairAISupported.Application.Features.Invoices.Profiles;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using InvoiceEntity = MagicCarRepairAISupported.Domain.Entities.Invoice;

namespace MagicCarRepairAISupported.Application.Tests.Features.Invoices.Profiles;

public class InvoiceMappingProfileTests
{
    [Fact]
    public void GenerateInvoiceFromWorkOrder_maps_entity_id_to_invoice_id()
    {
        var configuration = TestSupport.AutoMapperConfigurationFactory.Create(cfg => cfg.AddProfile<InvoiceMappingProfile>());
        var mapper = configuration.CreateMapper();

        var invoice = new InvoiceEntity
        {
            Id = 42,
            InvoiceNumber = "INV-TEST",
            WorkOrderId = 7,
            Status = InvoiceStatus.Pending,
            InvoiceDate = DateTime.UtcNow,
            ClientId = 1,
        };

        var response = mapper.Map<MagicCarRepairAISupported.Application.Features.Invoices.Commands.GenerateFromWorkOrder.GenerateInvoiceFromWorkOrderResponse>(invoice);

        response.InvoiceId.Should().Be(42);
    }
}
