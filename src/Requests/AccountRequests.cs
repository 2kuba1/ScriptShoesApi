using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesAPI.Middleware;
using ScriptShoesCQRS.Features.Users.Commands.AddProfilePicture;
using ScriptShoesCQRS.Features.Users.Commands.CreateUser;
using ScriptShoesCQRS.Features.Users.Commands.DeleteProfilePicture;
using ScriptShoesCQRS.Features.Users.Commands.SendEmailWithActivationCode;
using ScriptShoesCQRS.Features.Users.Commands.VerifyEmail;
using ScriptShoesCQRS.Features.Users.Queries.Login;
using ScriptShoesCQRS.Features.Users.Queries.RefreshToken;
using ScriptShoesCQRS.Features.Users.UsersValidators;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesAPI.Requests;

public static class AccountRequests
{
    public static WebApplication RegisterAccountEndpoints(this WebApplication app)
    {
        const string pattern = "api/account/";
        
        app.MapPost($"{pattern}register", Register)
            .Produces<CreateUserCommand>()
            .Accepts<CreateUserCommand>("application/json")
            .WithValidator<CreateUserCommand>()
            .WithTags("Account")
            .WithValidator<CreateUserCommandValidator>();

        app.MapPost($"{pattern}login", Login)
            .Produces<LoginQuery>()
            .Accepts<CreateUserCommand>("application/json")
            .WithValidator<LoginQuery>()
            .WithTags("Account");

        app.MapPost($"{pattern}sendEmailWithActivationCode", SendEmailWithActivationCode)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Account");
            

        app.MapPost($"{pattern}verifyEmailCode", VerifyEmailCode)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Account");

        app.MapPost($"{pattern}refreshToken", RefreshToken)
            .Produces<AuthenticationUserResponse>()
            .Accepts<RefreshTokenQuery>("application/json")
            .WithTags("Account");

        app.MapPost($"{pattern}addProfilePicture", AddProfilePicture)
            .Produces(StatusCodes.Status204NoContent)
            .Accepts<IFormFile>("multipart/form-data")
            .WithTags("Account");

        app.MapDelete($"{pattern}deleteProfilePicture", DeleteProfilePicture)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Account");
        
        return app;
    }

    private static async Task<IResult> Register(IMediator mediator, CreateUserCommand command)
    {
        await mediator.Send(command);
        return Results.NoContent();
    }

    private static async Task<IResult> Login(IMediator mediator, LoginQuery query)
    {
        var results = await mediator.Send(query);
        return Results.Ok(results);
    }

    private static async Task<IResult> RefreshToken(ISender mediator, RefreshTokenQuery query)
    {
        var results = await mediator.Send(query);
        return Results.Ok(results);
    } 
    
    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> SendEmailWithActivationCode(ISender mediator)
    {
        await mediator.Send(new SendEmailWithActivationCodeCommand()
        {
            Subject = "Script shoes verification email"
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> VerifyEmailCode(IMediator mediator, [FromQuery]string code)
    {
        await mediator.Send(new VerifyEmailCode()
        {
            Code = code
        });
        return Results.NoContent();
    }
    
    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> AddProfilePicture(ISender mediator, [FromForm]IFormFile file)
    {
        await mediator.Send(new AddProfilePictureCommand()
        {
            File = file
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> DeleteProfilePicture(ISender mediator)
    {
        await mediator.Send(new DeleteProfilePictureCommand());
        return Results.NoContent();
    }
}