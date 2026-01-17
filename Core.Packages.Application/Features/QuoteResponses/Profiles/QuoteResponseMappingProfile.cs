using AutoMapper;
using MagicCarRepairAISupported.Application.Features.QuoteResponses.Commands.Submit;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.QuoteResponses.Profiles
{
    public class QuoteResponseMappingProfile : Profile
    {
        public QuoteResponseMappingProfile()
        {
            CreateMap<QuoteResponse, SubmitQuoteResponseResponse>();
        }
    }
}

