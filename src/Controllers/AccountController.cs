using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.Users.Commands.CreateUser;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Controllers;


public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("/api/account/registerUser")]
    [AllowAnonymous]
    public async Task<ActionResult> Register([FromBody] CreateUserCommand command)
    {
        await _mediator.Send(command);
        
        return NoContent();
    }
}