﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoe;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoeImage;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoeMainImage;
using ScriptShoesCQRS.Features.AdminPanel.Commands.DeleteShoe;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateImage;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateMainImg;
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

    [HttpDelete]
    [Route("deleteShoe")]
    public async Task<ActionResult> DeleteShoe([FromQuery] string shoeName)
    {
        await _mediator.Send(new DeleteShoeCommand()
        {
            ShoeName = shoeName
        });
        return NoContent();
    }

    [HttpPost]
    [Route("addShoeMainImage")]
    public async Task<ActionResult> AddShoeMainImage([FromForm] IFormFile file, [FromQuery] string shoeName)
    {
        await _mediator.Send(new AddShoeMainImageCommand()
        {
            File = file,
            ShoeName = shoeName
        });
        return NoContent();
    }
    
    [HttpPost]
    [Route("addShoeImage")]
    public async Task<ActionResult> AddShoeImage([FromForm] IFormFile file, [FromQuery] string shoeName)
    {
        await _mediator.Send(new AddShoeImageCommand()
        {
            File = file,
            ShoeName = shoeName
        });
        return NoContent();
    }

    [HttpPatch]
    [Route("updateShoeMainImage")]
    public async Task<ActionResult> UpdateShoeMainImage([FromForm] IFormFile file, [FromQuery] string shoeName)
    {
        await _mediator.Send(new UpdateMainImgCommand()
        {
            File = file,
            ShoeName = shoeName
        });

        return NoContent();
    }

    [HttpPatch]
    [Route("updateShoeImage")]
    public async Task<ActionResult> UpdateShoeImage([FromForm] IFormFile file, [FromQuery] string shoeName)
    {
        await _mediator.Send(new UpdateImgCommand()
        {
            File = file,
            ShoeName = shoeName
        });

        return NoContent();
    }
}