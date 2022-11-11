using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.Users.Commands.CreateUser;

namespace ScriptShoesCQRS.Controllers;

[ApiController]
[Route("/api/account")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [AllowAnonymous]
    [Route("registerUser")]
    public async Task<ActionResult> RegisterUser([FromBody] CreateUserCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}