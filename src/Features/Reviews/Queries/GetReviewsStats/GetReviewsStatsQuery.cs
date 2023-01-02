using MediatR;
using ScriptShoesAPI.Models.Reviews;

namespace ScriptShoesAPI.Features.Reviews.Queries.GetReviewsStats;

public record GetReviewsStatsQuery : IRequest<ReviewsStatsDto>
{
    public int ShoeId { get; set; }
}