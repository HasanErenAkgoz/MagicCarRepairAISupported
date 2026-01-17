using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Features.AI.Commands.Chat;

namespace MagicCarRepairAISupported.Application.Common.AutoMapper
{
    public class ChatMappingProfile : Profile
    {
        public ChatMappingProfile()
        {
            CreateMap<ChatResponseDto, ChatResponse>()
                .ForMember(dest => dest.Response, opt => opt.MapFrom(src => src.Response))
                .ForMember(dest => dest.ResponseType, opt => opt.MapFrom(src => src.ResponseType))
                .ForMember(dest => dest.RelatedEntityId, opt => opt.MapFrom(src => src.RelatedEntityId))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.AdditionalData, opt => opt.MapFrom(src => src.AdditionalData))
                .ForMember(dest => dest.SuggestedActions, opt => opt.MapFrom(src => src.SuggestedActions));
        }
    }
}
