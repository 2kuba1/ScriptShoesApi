using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.Cart.Commands.AddToCart;
using ScriptShoesCQRS.Features.Cart.Commands.DeleteItemFromCart;
using ScriptShoesCQRS.Features.Cart.Queries.GetItemsFromCart;
using ScriptShoesCQRS.Models.Cart;

namespace ScriptShoesCQRS.Controllers;

[ApiController]
[Authorize(Roles = "User,Admin")]
[Route("/api/cart")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("addToCart/{shoeId:int}")]
    public async Task<ActionResult> AddToCart([FromRoute] int shoeId)
    {
        await _mediator.Send(new AddToCartCommand()
        {
            ShoeId = shoeId
        });

        return NoContent();
    }

    [HttpDelete]
    [Route("removeItemFromCart/{shoeId:int}")]
    public async Task<ActionResult> RemoveItemFromCart([FromRoute] int shoeId)
    {
        await _mediator.Send(new RemoveItemFromCartCommand()
        {
            ShoeId = shoeId
        });

        return NoContent();
    }

    [HttpGet]
    [Route("getItemsFromCart")]
    public async Task<ActionResult<IEnumerable<GetItemsFromCartDto>>> GetItemsFromCart()
    {
        var result = await _mediator.Send(new GetItemsFromCartQuery());
        return Ok(result);
    }
}