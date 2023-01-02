using MediatR;

namespace ScriptShoesAPI.Features.Favourites.Commands.AddToFavourites;
    
public record AddToFavouritesCommand : IRequest
{
    public int ShoeId { get; set; }
}