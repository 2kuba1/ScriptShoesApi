using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesAPI.Features.Cart.Commands.AddToCart;
using ScriptShoesAPI.Features.Cart.Commands.DeleteItemFromCart;
using ScriptShoesAPI.Features.Cart.Queries.GetItemsFromCart;
using ScriptShoesAPI.Models.Cart;

namespace ScriptShoesAPI.Endpoints;

public static class CartEndpoints
{
    public static WebApplication RegisterCartEndpoints(this WebApplication app)
    {
        const string pattern = "api/cart/";

        app.MapPost(pattern + "addToCart/{shoeId:int}", AddToCart)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Cart");
        
        app.MapDelete(pattern + "removeItemFromCart/{shoeId:int}", RemoveFromCart)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Cart");
        
        app.MapGet($"{pattern}getItemsFromCart", GetItemsFromCart)
            .Produces<IEnumerable<GetItemsFromCartDto>>()
            .WithTags("Cart");

        return app;
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> AddToCart(ISender mediator, [FromRoute] int shoeId)
    {
        await mediator.Send(new AddToCartCommand()
        {
            ShoeId = shoeId
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> RemoveFromCart(ISender mediator, [FromRoute] int shoeId)
    {
        await mediator.Send(new RemoveItemFromCartCommand()
        {
            ShoeId = shoeId
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> GetItemsFromCart(ISender mediator)
    {
        var results = await mediator.Send(new GetItemsFromCartQuery());
        return Results.Ok(results);
    }
}