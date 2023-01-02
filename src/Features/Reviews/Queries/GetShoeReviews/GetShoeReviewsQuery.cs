using MediatR;
using ScriptShoesAPI.Models.Reviews;

namespace ScriptShoesAPI.Features.Reviews.Queries.GetShoeReviews;

public record GetShoeReviewsQuery : IRequest<IEnumerable<ReviewsDto>>
{
    public int ShoeId { get; set; }
}