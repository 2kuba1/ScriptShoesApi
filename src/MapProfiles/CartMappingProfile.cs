using AutoMapper;
using ScriptShoesCQRS.Database.Entities;
using ScriptShoesCQRS.Models.Cart;

namespace ScriptShoesCQRS.MapProfiles;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<Shoes, GetItemsFromCartDto>();
    }
}