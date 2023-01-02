using MediatR;

namespace ScriptShoesAPI.Features.Cart.Commands.DeleteItemFromCart;

public record RemoveItemFromCartCommand : IRequest
{
    public int ShoeId { get; set; }
}