using AutoMapper;
using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Features.Users.Commands.CreateUser;

namespace ScriptShoesCQRS.MapProfiles;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<CreateUserCommand, User>();
    }
}