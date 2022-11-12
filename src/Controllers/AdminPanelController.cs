using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoe;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateShoe;

namespace ScriptShoesCQRS.Controllers;

[ApiController]
[Route("/api/adminPanel")]
[Authorize(Roles = "Admin")]
public class AdminPanelController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminPanelController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("createShoe")]
    public async Task<ActionResult> CreateShoe([FromBody] AddShoeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    [Route("updateShoe")]
    public async Task<ActionResult> UpdateShoe([FromBody] UpdateShoeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}