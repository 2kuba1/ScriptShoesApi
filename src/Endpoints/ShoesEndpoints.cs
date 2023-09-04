using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesAPI.Features.Shoes.Queries.GetAllShoes;
using ScriptShoesAPI.Features.Shoes.Queries.GetFilters;
using ScriptShoesAPI.Features.Shoes.Queries.GetShoesByName;
using ScriptShoesAPI.Features.Shoes.Queries.GetshoeWithContent;
using ScriptShoesAPI.Models.Shoes;

namespace ScriptShoesAPI.Endpoints;

public static class ShoesEndpoints
{
    public static WebApplication RegisterShoesEndpoints(this WebApplication app)
    {
        const string pattern = "api/shoes/";

        app.MapGet($"{pattern}getAllShoes", GetAll)
            .Produces<IEnumerable<GetAllShoesDto>>()
            .WithTags("Shoes");
            
        app.MapGet($"{pattern}getShoeByName", GetShoeByName)
            .Produces<IEnumerable<GetShoesByNameDto>>()
            .WithTags("Shoes");

        app.MapGet($"{pattern}getShoe", GetShoeWithContent)
            .Produces<GetShoeWithContentResponse>()
            .WithTags("Shoes");

        app.MapGet($"{pattern}getFilters", GetFilters)
            .Produces<GetFiltersDto>()
            .WithTags("Shoes");

        return app;
    }

    private static async Task<IResult> GetAll(ISender mediator)
    {
        var shoesList = await mediator.Send(new GetAllShoesQuery());
        return Results.Ok(shoesList);
    }

    private static async Task<IResult> GetShoeByName(ISender mediator, [FromQuery] string searchPhrase)
    {
        var results = await mediator.Send(new GetShoesByNameQuery()
        {
            SearchPhrase  = searchPhrase
        });
        return Results.Ok(results);
    }

    private static async Task<IResult> GetShoeWithContent(ISender mediator,[FromQuery] string shoeName)
    {
        var result = await mediator.Send(new GetShoeWithContentQuery()
        {
            ShoeName = shoeName
        });
        return Results.Ok(result);
    }

    private static async Task<IResult> GetFilters(ISender mediator)
    {
        var results = await mediator.Send(new GetFiltersQuery());
        return Results.Ok(results);
    }
}