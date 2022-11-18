using MediatR;
using ScriptShoesCQRS.Models.Cart;

namespace ScriptShoesCQRS.Features.Cart.Commands.Queries.GetItemsFromCart;

public class GetItemsFromCartQuery : IRequest<IEnumerable<GetItemsFromCartDto>>
{
    
}