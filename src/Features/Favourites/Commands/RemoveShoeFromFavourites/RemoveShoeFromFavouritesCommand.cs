using MediatR;

namespace ScriptShoesAPI.Features.Favourites.Commands.RemoveShoeFromFavourites;

public record RemoveShoeFromFavouritesCommand : IRequest
{
    public int ShoeId { get; set; }
}