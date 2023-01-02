using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesAPI.Validators;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoe;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoeImage;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoeMainImage;
using ScriptShoesCQRS.Features.AdminPanel.Commands.DeleteShoe;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateImage;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateMainImg;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateShoe;

namespace ScriptShoesAPI.Requests;

public static class AdminPanelRequests
{
    public static WebApplication RegisterAdminPanelEndpoints(this WebApplication app)
    {
        const string pattern = "api/adminPanel/";

        app.MapPost($"{pattern}createShoe", CreateShoe)
            .Produces(StatusCodes.Status204NoContent)
            .Accepts<AddShoeCommand>("application/json")
            .WithTags("AdminPanel")
            .WithValidator<AddShoeCommand>();

        app.MapPut($"{pattern}updateShoe", UpdateShoe)
            .Produces(StatusCodes.Status204NoContent)
            .Accepts<UpdateShoeCommand>("application/json")
            .WithTags("AdminPanel")
            .WithValidator<UpdateShoeCommand>();

        app.MapDelete($"{pattern}deleteShoe", DeleteShoe)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("AdminPanel");

        app.MapPost($"{pattern}addShoeMainImage", AddShoeMainImage)
            .Produces(StatusCodes.Status204NoContent)
            .Accepts<IFormFile>("multipart/form-data")
            .WithTags("AdminPanel");

        app.MapPost($"{pattern}addShoeImage", AddShoeImage)
            .Produces(StatusCodes.Status204NoContent)
            .Accepts<IFormFile>("multipart/form-data")
            .WithTags("AdminPanel");

        app.MapPatch($"{pattern}updateShoeMainImage", UpdateShoeMainImage)
            .Produces(StatusCodes.Status204NoContent)
            .Accepts<IFormFile>("multipart/form-data")
            .WithTags("AdminPanel");

        app.MapPatch($"{pattern}updateShoeImage", UpdateShoeImage)
            .Produces(StatusCodes.Status204NoContent)
            .Accepts<IFormFile>("multipart/form-data")
            .WithTags("AdminPanel");

        return app;
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> CreateShoe(IMediator mediator,AddShoeCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> UpdateShoe(ISender mediator, UpdateShoeCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> DeleteShoe(ISender mediator, [FromQuery] string shoeName)
    {
        await mediator.Send(new DeleteShoeCommand()
        {
            ShoeName = shoeName
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> AddShoeMainImage(ISender mediator,[FromForm] IFormFile file, [FromQuery]string shoeName)
    {
        await mediator.Send(new AddShoeMainImageCommand()
        {
            File = file,
            ShoeName = shoeName
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> AddShoeImage(ISender mediator, [FromForm] IFormFile file,
        [FromQuery] string shoeName)
    {
        await mediator.Send(new AddShoeImageCommand()
        {
            File = file,
            ShoeName = shoeName
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> UpdateShoeMainImage(ISender mediator,[FromForm] IFormFile file, string shoeName)
    {
        await mediator.Send(new UpdateMainImgCommand()
        {
            File = file,
            ShoeName = shoeName
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> UpdateShoeImage(ISender mediator, [FromForm] IFormFile file, string shoeName)
    {
        await mediator.Send(new UpdateImgCommand()
        {
            File = file,
            ShoeName = shoeName
        });
        return Results.NoContent();
    }
}