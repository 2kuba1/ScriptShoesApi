using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Queries.GetLikedReviews;

public class GetLikedReviewsQuery : IRequest<IEnumerable<int>>
{
    public int ShoeId { get; set; }
}