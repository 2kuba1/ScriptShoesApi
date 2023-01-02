using MediatR;
using ScriptShoesAPI.Models.Cart;

namespace ScriptShoesAPI.Features.Cart.Queries.GetItemsFromCart;

public record GetItemsFromCartQuery : IRequest<IEnumerable<GetItemsFromCartDto>>
{
    
}