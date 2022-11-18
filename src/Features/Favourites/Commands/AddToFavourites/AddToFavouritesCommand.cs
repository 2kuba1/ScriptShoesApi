using MediatR;

namespace ScriptShoesCQRS.Features.Favourites.Commands.AddToFavourites;

public class AddToFavouritesCommand : IRequest
{
    public int ShoeId { get; set; }
}