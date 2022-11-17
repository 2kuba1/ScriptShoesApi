using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Queries.GetAvailableReviews;

public class GetAvailableReviewsQuery : IRequest<IEnumerable<int>>
{
    public int ShoeId { get; set; }
}