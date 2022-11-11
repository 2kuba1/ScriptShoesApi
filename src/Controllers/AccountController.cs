﻿using System.Runtime.InteropServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.Users.Commands.CreateUser;
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
}