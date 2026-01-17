using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Ratings.Commands.CreateRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Commands.ReplyToRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Commands.ModerateRating;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Profiles
{
    public class RatingMappingProfile : Profile
    {
        public RatingMappingProfile()
        {
            CreateMap<ServiceRating, CreateRatingResponse>();
            CreateMap<ServiceRating, ReplyToRatingResponse>();
            CreateMap<ServiceRating, ModerateRatingResponse>();
        }
    }
}
