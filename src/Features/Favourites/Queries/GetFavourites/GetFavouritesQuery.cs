using MediatR;
using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Models.Favourites;

namespace ScriptShoesCQRS.Features.Favourites.Queries.GetFavourites;

public record GetFavouritesQuery : IRequest<IEnumerable<GetFavouritesDto>>
{
    
}