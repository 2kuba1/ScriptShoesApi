using AutoMapper;
using ScriptShoesAPI.Models.Favourites;
using ScriptShoesCQRS.Database.Entities;

namespace ScriptShoesAPI.MapProfiles;

public class FavouritesMappingProfile : Profile
{
    public FavouritesMappingProfile()
    {
        CreateMap<Shoes, GetFavouritesDto>();
    }
}