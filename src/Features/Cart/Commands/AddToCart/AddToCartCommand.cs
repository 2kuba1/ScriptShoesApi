using MediatR;

namespace ScriptShoesAPI.Features.Cart.Commands.AddToCart;

public record AddToCartCommand : IRequest
{
    public int ShoeId { get; set; }
}