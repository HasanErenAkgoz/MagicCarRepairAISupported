using AutoMapper;
using MagicCarRepairAISupported.Application.Common.AutoMapper;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Translate.Commands.Create
{
    public class CreateTranslateCommand : IRequest<IResult>,IMapFrom<Domain.Entities.Translation>
    {
        public string Language { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTranslateCommand, Domain.Entities.Translation>();
        }
    }
}
