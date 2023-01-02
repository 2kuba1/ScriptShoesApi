using AutoMapper;
using ScriptShoesAPI.Database.Entities;
using ScriptShoesAPI.Features.Users.Commands.CreateUser;

namespace ScriptShoesAPI.MapProfiles;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<CreateUserCommand, User>();
    }
}