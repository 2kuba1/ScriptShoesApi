using AutoMapper;
using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Database.Entities;
using ScriptShoesCQRS.Models.Reviews;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.MapProfiles;

public class ShoesMappingProfile : Profile
{
    public ShoesMappingProfile()
    {
        CreateMap<Shoes, GetShoesByNameDto>();
        CreateMap<Shoes, GetShoeWithContentDto>();
        CreateMap<Reviews, ReviewsDto>().ForMember(s => s.ProfilePicture, c => c.MapFrom(s => s.Users.ProfilePictureUrl));
        CreateMap<Shoes, GetAllShoesDto>().ForMember(s => s.Reviews, c => c.MapFrom(d => d.Reviews));
    }
}