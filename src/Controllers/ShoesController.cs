using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.Shoes.Queries.GetAllShoes;
using ScriptShoesCQRS.Features.Shoes.Queries.GetFilters;
using ScriptShoesCQRS.Features.Shoes.Queries.GetShoesByName;
using ScriptShoesCQRS.Features.Shoes.Queries.GetShoeWithContent;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Controllers;

[ApiController]
[Route("/api/shoes")]
public class ShoesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShoesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("getAllShoes")]
    public async Task<ActionResult<IEnumerable<GetAllShoesDto>>> GetAll()
    {
        var shoesList = await _mediator.Send(new GetAllShoesQuery());
        return Ok(shoesList);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("getShoeByName")]
    public async Task<ActionResult<IEnumerable<GetShoesByNameDto>>> GetByName([FromQuery]string searchPhrase)
    {
        var results = await _mediator.Send(new GetShoesByNameQuery()
        {
          SearchPhrase  = searchPhrase
        });
        return Ok(results);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("getShoe")]
    public async Task<ActionResult<GetShoeWithContentResponse>> GetShoeWithContent([FromQuery]string shoeName)
    {
        var result = await _mediator.Send(new GetShoeWithContentQuery()
        {
            ShoeName = shoeName
        });
        
        return Ok(result);
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("getFilters")]
    public async Task<ActionResult<GetFiltersDto>> GteFilters()
    {
        var results = await _mediator.Send(new GetFiltersQuery());
        return Ok(results);
    }
}