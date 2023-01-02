using AutoMapper;
using ScriptShoesAPI.Database.Entities;
using ScriptShoesAPI.Features.AdminPanel.Commands.AddShoe;
using ScriptShoesAPI.Models.Reviews;
using ScriptShoesAPI.Models.Shoes;
using ScriptShoesCQRS.Database.Entities;

namespace ScriptShoesAPI.MapProfiles;

public class ShoesMappingProfile : Profile
{
    public ShoesMappingProfile()
    {
        CreateMap<Shoes, GetShoesByNameDto>();
        CreateMap<Shoes, GetShoeWithContentDto>();
        CreateMap<Reviews, ReviewsDto>().ForMember(s => s.ProfilePicture, c => c.MapFrom(s => s.Users.ProfilePictureUrl));
        CreateMap<Shoes, GetAllShoesDto>().ForMember(s => s.Reviews, c => c.MapFrom(d => d.Reviews));
        CreateMap<AddShoeCommand, Shoes>();
    }
}