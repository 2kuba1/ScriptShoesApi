using MediatR;

namespace ScriptShoesCQRS.Features.Cart.Commands.AddToCart;

public record AddToCartCommand : IRequest
{
    public int ShoeId { get; set; }
}