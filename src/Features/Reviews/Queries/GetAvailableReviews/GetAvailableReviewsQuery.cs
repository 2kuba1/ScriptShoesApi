using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Queries.GetAvailableReviews;

public record GetAvailableReviewsQuery : IRequest<IEnumerable<int>>
{
    public int ShoeId { get; set; }
}