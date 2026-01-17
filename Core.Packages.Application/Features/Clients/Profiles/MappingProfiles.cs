using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Clients.Commands.CreateClient;
using MagicCarRepairAISupported.Application.Features.Clients.Queries.GetClientById;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.Clients.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateClientCommand, Client>();
            CreateMap<Client, CreateClientResponse>();
            CreateMap<Client, GetClientByIdResponse>();
        }
    }
}

