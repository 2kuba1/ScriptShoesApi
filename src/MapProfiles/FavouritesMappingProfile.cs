using AutoMapper;
using ScriptShoesCQRS.Database.Entities;
using ScriptShoesCQRS.Models.Favourites;

namespace ScriptShoesCQRS.MapProfiles;

public class FavouritesMappingProfile : Profile
{
    public FavouritesMappingProfile()
    {
        CreateMap<Shoes, GetFavouritesDto>();
    }
}