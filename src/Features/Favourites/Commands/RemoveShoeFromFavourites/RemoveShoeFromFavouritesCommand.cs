using MediatR;

namespace ScriptShoesCQRS.Features.Favourites.Commands.RemoveShoeFromFavourites;

public record RemoveShoeFromFavouritesCommand : IRequest
{
    public int ShoeId { get; set; }
}