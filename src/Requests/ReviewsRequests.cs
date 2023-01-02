using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesAPI.Features.Reviews.Commands.AddLikeToReview;
using ScriptShoesAPI.Features.Reviews.Commands.CreateReview;
using ScriptShoesAPI.Features.Reviews.Commands.DeleteReview;
using ScriptShoesAPI.Features.Reviews.Commands.RemoveLike;
using ScriptShoesAPI.Features.Reviews.Commands.UpdateReview;
using ScriptShoesAPI.Features.Reviews.Commands.UpdateReviewLike;
using ScriptShoesAPI.Features.Reviews.Queries.GetAvailableReviews;
using ScriptShoesAPI.Features.Reviews.Queries.GetLikedReviews;
using ScriptShoesAPI.Features.Reviews.Queries.GetShoeReviews;
using ScriptShoesAPI.Models.Reviews;
using ScriptShoesAPI.Validators;

namespace ScriptShoesAPI.Requests;

public static class ReviewsRequests
{
    public static WebApplication RegisterReviewsEndpoints(this WebApplication app)
    {
        const string pattern = "api/{shoeId:int}/reviews";

        app.MapPost($"{pattern}createReview", CreateReview)
            .Produces<CreateReviewDto>()
            .Accepts<CreateReviewDto>("application/json")
            .WithTags("Reviews")
            .WithValidator<CreateReviewCommand>();

        app.MapGet($"{pattern}getReviewsStats", GetReviewsStats)
            .Produces<ReviewsStatsDto>()
            .Accepts<ReviewsStatsDto>("application/json")
            .WithTags("Reviews");

        app.MapGet($"{pattern}getUserLikedReviews", GetLikedReviews)
            .Produces<IEnumerable<int>>()
            .WithTags("Reviews");

        app.MapPost(pattern + "addLikeToReview/{reviewId:int}", LikeReview)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Reviews");

        app.MapDelete(pattern + "removeLikeFromReview/{reviewId:int}", RemoveLike)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Reviews");
        
        app.MapPut(pattern + "updateReviewLike/{reviewId:int}", UpdateLike)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Reviews");

        app.MapPut(pattern + "updateReview/{reviewId:int}", UpdateReview)
            .Produces(StatusCodes.Status204NoContent)
            .Accepts<UpdateReviewDto>("application/json")
            .WithTags("Reviews")
            .WithValidator<UpdateReviewCommand>();

        app.MapDelete(pattern + "deleteReview/{reviewId:int}", DeleteReview)
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Reviews");
        
        app.MapGet($"{pattern}getAvailableUserReviews", GetAvailableUserReviews)
            .Produces<IEnumerable<int>>()
            .WithTags("Reviews");
        
        app.MapGet($"{pattern}getShoeReviews", GetShoeReviews)
            .Produces<IEnumerable<int>>()
            .WithTags("Reviews");
        
        return app;
    }
    
     [Authorize(Roles = "User,Admin")]
     private static async Task<IResult> CreateReview(ISender mediator, [FromRoute] int shoeId, CreateReviewDto dto)
    {
        var results = await mediator.Send(new CreateReviewCommand()
        {
            Rate = dto.Rate,
            Review = dto.Review,
            Title = dto.Title,
            ShoeId = shoeId
        });
        return Results.Ok(results);
    }

    private static async Task<IResult> GetReviewsStats(ISender mediator, [FromRoute] int shoeId)
    {
        var results = await mediator.Send(new GetLikedReviewsQuery()
        {
            ShoeId = shoeId
        });
        return Results.Ok(results);
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> GetLikedReviews(ISender mediator, [FromRoute] int shoeId)
    {
        var results = await mediator.Send(new GetLikedReviewsQuery()
        {
            ShoeId = shoeId
        });
        return Results.Ok(results);
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> LikeReview(ISender mediator, [FromRoute] int shoeId, [FromRoute] int reviewId)
    {
        var results = await mediator.Send(new AddLikeToReviewCommand
        {
            ReviewId = reviewId,
            ShoeId = shoeId
        });
        return Results.Ok(results);
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> RemoveLike(ISender mediator, [FromRoute] int shoeId, [FromRoute] int reviewId)
    {
        await mediator.Send(new RemoveLikeCommand()
        {
            ReviewId = reviewId,
            ShoeId = shoeId
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> UpdateLike(ISender mediator, [FromRoute] int shoeId, [FromRoute] int reviewId,
        int likes)
    {
        await mediator.Send(new UpdateReviewLikeCommand()
        {
            Likes = likes,
            ReviewId = reviewId,
            ShoeId = shoeId
        });
        return Results.NoContent();
    }
    
    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> UpdateReview(ISender mediator, [FromRoute] int shoeId, [FromRoute] int reviewId,
        [FromBody] UpdateReviewDto dto)
    {
        await mediator.Send(new UpdateReviewCommand()
        {
            Rate = dto.Rate,
            Review = dto.Review,
            Title = dto.Title,
            ReviewId = reviewId,
            ShoeId = shoeId
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> DeleteReview(ISender mediator, [FromRoute] int shoeId, [FromRoute] int reviewId)
    {
        await mediator.Send(new DeleteReviewCommand()
        {
            ReviewId = reviewId,
            ShoeId = shoeId
        });
        return Results.NoContent();
    }

    [Authorize(Roles = "User,Admin")]
    private static async Task<IResult> GetAvailableUserReviews(ISender mediator, [FromRoute] int shoeId)
    {
        var results = await mediator.Send(new GetAvailableReviewsQuery()
        {
            ShoeId = shoeId
        });
        return Results.Ok(results);
    }

    private static async Task<IResult> GetShoeReviews(ISender mediator,[FromRoute] int shoeId)
    {
        var results = await mediator.Send(new GetShoeReviewsQuery()
        {
            ShoeId = shoeId
        });
        return Results.Ok(results);
    }
}