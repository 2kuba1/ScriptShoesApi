using MediatR;
using ScriptShoesCQRS.Models.Cart;

namespace ScriptShoesCQRS.Features.Cart.Commands.Queries.GetItemsFromCart;

public record GetItemsFromCartQuery : IRequest<IEnumerable<GetItemsFromCartDto>>
{
    
}