using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.GenerateFromWorkOrder;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.UpdateStatus;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetById;
using InvoiceEntity = MagicCarRepairAISupported.Domain.Entities.Invoice;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Profiles
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<InvoiceEntity, CreateInvoiceResponse>();
            CreateMap<InvoiceEntity, GenerateInvoiceFromWorkOrderResponse>();
            CreateMap<InvoiceEntity, UpdateInvoiceStatusResponse>();
            CreateMap<InvoiceEntity, GetAllInvoicesResponse>();
            CreateMap<InvoiceEntity, GetInvoiceByIdResponse>();
        }
    }
}

