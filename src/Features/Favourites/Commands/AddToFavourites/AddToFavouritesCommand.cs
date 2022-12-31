using MediatR;

namespace ScriptShoesCQRS.Features.Favourites.Commands.AddToFavourites;

public record AddToFavouritesCommand : IRequest
{
    public int ShoeId { get; set; }
}