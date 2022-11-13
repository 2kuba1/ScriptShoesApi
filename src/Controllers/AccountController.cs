using System.Runtime.InteropServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.Users.Commands.AddProfilePicture;
using ScriptShoesCQRS.Features.Users.Commands.CreateUser;
using ScriptShoesCQRS.Features.Users.Commands.DeleteProfilePicture;
using ScriptShoesCQRS.Features.Users.Commands.SendEmailWithActivationCode;
using ScriptShoesCQRS.Features.Users.Commands.SendEmailWithNewActivationCode;
using ScriptShoesCQRS.Features.Users.Commands.VerifyEmail;
using ScriptShoesCQRS.Features.Users.Queries.Login;
using ScriptShoesCQRS.Features.Users.Queries.RefreshToken;
using ScriptShoesCQRS.Models.Users;

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

    [HttpPost]
    [AllowAnonymous]
    [Route("loginUser")]
    public async Task<ActionResult<AuthenticationUserResponse>> LoginUser([FromBody] LoginQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route(("refreshToken"))]
    public async Task<ActionResult<AuthenticationUserResponse>> RefreshToken([FromQuery] RefreshTokenQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    [Route("sendEmailWithActivationCode")]
    public async Task<ActionResult> SendEmailWithActivationCode()
    {
        await _mediator.Send(new SendEmailWithActivationCodeCommand()
        {
            Subject = "Script shoes verification email"
        });

        return new NoContentResult();
    }

    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    [Route("sendEmailWithNewActivationCode")]
    public async Task<ActionResult> SendEmailWithNewActivationCode()
    {
        await _mediator.Send(new SendEmailWithNewActivationCodeCommand());
        return new NoContentResult();
    }
    
    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    [Route("verifyEmailCode")]
    public async Task<ActionResult> VerifyEmailCode([FromQuery] string code)
    {
        await _mediator.Send(new VerifyEmailCode()
        {
            Code = code
        });

        return NoContent();
    }
    
    [HttpPost]
    [Route("addProfilePicture")]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult> AddProfilePicture([FromForm] AddProfilePictureCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete]
    [Route("deleteProfilePicture")]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult> DeleteProfilePicture()
    {
        await _mediator.Send(new DeleteProfilePictureCommand());
        return Ok();
    }
}