using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesAPI.Features.Favourites.Commands.AddToFavourites;
using ScriptShoesAPI.Features.Favourites.Commands.RemoveShoeFromFavourites;
using ScriptShoesAPI.Features.Favourites.Queries.GetFavourites;
using ScriptShoesAPI.Models.Favourites;

namespace ScriptShoesAPI.Requests;

public static class FavoritesRequests
{
    public static WebApplication RegisterFavoritesEndpoints(this WebApplication app)
    {
        const string pattern = "api/favorites";

        app.MapPost(pattern + "addToFavorites/{shoeId:int}", AddToFavorites)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Favorites");
        
        app.MapDelete(pattern + "deleteFromFavorites/{shoeId:int}", RemoveFromFavorites)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Favorites");
        
        app.MapGet($"{pattern}getFavorites", GetFavorites)
            .Produces<IEnumerable<GetFavouritesDto>>()
            .WithTags("Favorites");

        return app;
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> AddToFavorites(ISender mediator,[FromRoute] int shoeId)
    {
        await mediator.Send(new AddToFavouritesCommand()
        {
            ShoeId = shoeId
        });
        return Results.NoContent();
    }
    
    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> RemoveFromFavorites(ISender mediator,[FromRoute] int shoeId)
    {
        await mediator.Send(new RemoveShoeFromFavouritesCommand()
        {
            ShoeId = shoeId
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> GetFavorites(ISender mediator)
    {
        var results = await mediator.Send(new GetFavouritesQuery());
        return Results.Ok(results);
    }
}