using AutoMapper;
using ScriptShoesAPI.Models.Cart;
using ScriptShoesCQRS.Database.Entities;

namespace ScriptShoesAPI.MapProfiles;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<Shoes, GetItemsFromCartDto>();
    }
}