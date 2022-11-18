using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Queries.GetLikedReviews;

public record GetLikedReviewsQuery : IRequest<IEnumerable<int>>
{
    public int ShoeId { get; set; }
}