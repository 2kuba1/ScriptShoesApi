using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.Favourites.Commands.AddToFavourites;
using ScriptShoesCQRS.Features.Favourites.Commands.RemoveShoeFromFavourites;
using ScriptShoesCQRS.Features.Favourites.Queries.GetFavourites;
using ScriptShoesCQRS.Models.Favourites;

namespace ScriptShoesCQRS.Controllers;

[ApiController]
[Authorize(Roles = "User,Admin")]
[Route("/api/favourites")]
public class FavouritesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FavouritesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("addToFavourites/{shoeId:int}")]
    public async Task<ActionResult> AddToFavourites([FromRoute] int shoeId)
    {
        await _mediator.Send(new AddToFavouritesCommand()
        {
            ShoeId = shoeId
        });
        
        return NoContent();
    }

    [HttpDelete]
    [Route("deleteFromFavourites/{shoeId:int}")]
    public async Task<ActionResult> RemoveFromFavourites([FromRoute] int shoeId)
    {
        await _mediator.Send(new RemoveShoeFromFavouritesCommand()
        {
            ShoeId = shoeId
        });

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetFavouritesDto>>> GetFavourites()
    {
        var results = await _mediator.Send(new GetFavouritesQuery());

        return Ok(results);
    }
}