﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Features.Reviews.Commands.AddLikeToReview;
using ScriptShoesCQRS.Features.Reviews.Commands.CreateReview;
using ScriptShoesCQRS.Features.Reviews.Commands.DeleteReview;
using ScriptShoesCQRS.Features.Reviews.Commands.RemoveLike;
using ScriptShoesCQRS.Features.Reviews.Commands.UpdateReview;
using ScriptShoesCQRS.Features.Reviews.Commands.UpdateReviewLike;
using ScriptShoesCQRS.Features.Reviews.Queries.GetAvailableReviews;
using ScriptShoesCQRS.Features.Reviews.Queries.GetLikedReviews;
using ScriptShoesCQRS.Features.Reviews.Queries.GetReviewsStats;
using ScriptShoesCQRS.Models.Reviews;

namespace ScriptShoesCQRS.Controllers;

[ApiController]
[Route("/api/{shoeId:int}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    [Route("createReview")]
    public async Task<Reviews> CreateReview([FromRoute] int shoeId, [FromBody] CreateReviewDto dto)
    {
        var result = await _mediator.Send(new CreateReviewCommand()
        {
            Rate = dto.Rate,
            Review = dto.Review,
            Title = dto.Title,
            ShoeId = shoeId
        });
        
        return result;
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("getReviewsStats")]
    public async Task<ActionResult<ReviewsStatsDto>> GetReviewsStats([FromRoute] int shoeId)
    {
        var result = await _mediator.Send(new GetReviewsStatsQuery()
        {
            ShoeId = shoeId
        });

        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    [Route("getUserLikedReviews")]
    public async Task<ActionResult<IEnumerable<int>>> GetLikedReviews([FromRoute] int shoeId)
    {
        var result = await _mediator.Send(new GetLikedReviewsQuery()
        {
            ShoeId = shoeId
        });

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    [Route("addLikeToReview/{reviewId:int}")]
    public async Task<ActionResult> AddLikeToReview([FromRoute] int shoeId, [FromRoute] int reviewId)
    {
        await _mediator.Send(new AddLikeToReviewCommand()
        {
            ReviewId = reviewId,
            ShoeId = shoeId
        });

        return NoContent();
    }

    [HttpDelete]
    [Authorize(Roles = "User,Admin")]
    [Route("removeLikeFromReview/{reviewId:int}")]
    public async Task<ActionResult> RemoveLikeFromReview([FromRoute] int shoeId, [FromRoute] int reviewId)
    {
        await _mediator.Send(new RemoveLikeCommand()
        {
            ReviewId = reviewId,
            ShoeId = shoeId
        });

        return NoContent();
    }

    [HttpPut]
    [Authorize(Roles = "User,Admin")]
    [Route("updateReviewLike/{reviewId:int}")] //toggle like implementation
    public async Task<ActionResult> UpdateReviewLike([FromRoute] int shoeId, [FromRoute] int reviewId, int likes)
    {
        await _mediator.Send(new UpdateReviewLikeCommand()
        {
            Likes = likes,
            ReviewId = reviewId,
            ShoeId = shoeId
        });

        return NoContent();
    }

    [HttpPut]
    [Authorize(Roles = "User,Admin")]
    [Route("updateReview/{reviewId:int}")]
    public async Task<ActionResult> UpdateReview([FromRoute] int shoeId, [FromRoute] int reviewId,
        [FromBody] UpdateReviewDto dto)
    {
        await _mediator.Send(new UpdateReviewCommand()
        {
            Rate = dto.Rate,
            Review = dto.Review,
            Title = dto.Title,
            ReviewId = reviewId,
            ShoeId = shoeId
        });

        return NoContent();
    }

    [HttpDelete]
    [Authorize(Roles = "User,Admin")]
    [Route("deleteReview/{reviewId:int}")]
    public async Task<ActionResult> DeleteReview([FromRoute] int shoeId, [FromRoute] int reviewId)
    {
        await _mediator.Send(new DeleteReviewCommand()
        {
            ReviewId = reviewId,
            ShoeId = shoeId
        });

        return NoContent();
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    [Route("getAvailableUserReviews")]
    public async Task<ActionResult<IEnumerable<int>>> GetAvailableUserReviews([FromRoute] int shoeId)
    {
        var result = await _mediator.Send(new GetAvailableReviewsQuery()
        {
            ShoeId = shoeId
        });

        return Ok(result);
    }

}