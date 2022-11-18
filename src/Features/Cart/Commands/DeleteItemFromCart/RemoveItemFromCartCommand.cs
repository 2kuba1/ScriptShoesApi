using MediatR;

namespace ScriptShoesCQRS.Features.Cart.Commands.DeleteItemFromCart;

public record RemoveItemFromCartCommand : IRequest
{
    public int ShoeId { get; set; }
}