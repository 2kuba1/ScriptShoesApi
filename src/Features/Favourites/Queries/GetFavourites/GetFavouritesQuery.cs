using MediatR;
using ScriptShoesAPI.Models.Favourites;

namespace ScriptShoesAPI.Features.Favourites.Queries.GetFavourites;

public record GetFavouritesQuery : IRequest<IEnumerable<GetFavouritesDto>>
{
    
}