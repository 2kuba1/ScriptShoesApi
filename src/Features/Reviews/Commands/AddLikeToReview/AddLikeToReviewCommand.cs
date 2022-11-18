using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Commands.AddLikeToReview;

public record AddLikeToReviewCommand : IRequest
{
    public int ShoeId { get; set; }
    public int ReviewId { get; set; }
}