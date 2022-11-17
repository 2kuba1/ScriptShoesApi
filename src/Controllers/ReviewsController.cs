using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Features.Reviews.Commands.CreateReview;
using ScriptShoesCQRS.Features.Reviews.Queries;
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
    public async Task<ReviewsStatsDto> GetReviewsStats([FromRoute] int shoeId)
    {
        var result = await _mediator.Send(new GetReviewsStatsQuery()
        {
            ShoeId = shoeId
        });

        return result;
    }
}