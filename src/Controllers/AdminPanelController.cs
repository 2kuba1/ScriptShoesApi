using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoe;

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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateShoe([FromBody] AddShoeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}