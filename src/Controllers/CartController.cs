using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.Cart.Commands.AddToCart;

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
    [Route("addToCart")]
    public async Task<ActionResult> AddToCart([FromRoute] int shoeId)
    {
        await _mediator.Send(new AddToCartCommand()
        {
            ShoeId = shoeId
        });

        return NoContent();
    }
}