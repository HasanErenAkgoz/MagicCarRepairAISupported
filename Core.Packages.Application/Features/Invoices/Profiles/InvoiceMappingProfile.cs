using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.GenerateFromWorkOrder;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.UpdateStatus;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;
using InvoiceEntity = MagicCarRepairAISupported.Domain.Entities.Invoice;
using GetByIdInvoiceItemDto = MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetById.InvoiceItemDto;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Profiles
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<InvoiceEntity, CreateInvoiceResponse>();
            CreateMap<InvoiceEntity, GenerateInvoiceFromWorkOrderResponse>()
                .ForMember(d => d.InvoiceId, opt => opt.MapFrom(s => s.Id));
            CreateMap<InvoiceEntity, UpdateInvoiceStatusResponse>();
            CreateMap<InvoiceEntity, GetAllInvoicesResponse>();
            CreateMap<InvoiceItem, GetByIdInvoiceItemDto>();
            CreateMap<InvoiceEntity, GetInvoiceByIdResponse>();
        }
    }
}

