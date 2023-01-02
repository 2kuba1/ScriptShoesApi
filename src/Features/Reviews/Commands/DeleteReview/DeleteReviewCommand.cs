using MediatR;

namespace ScriptShoesAPI.Features.Reviews.Commands.DeleteReview;

public record DeleteReviewCommand : IRequest
{
    public int ShoeId { get; set; }
    public int ReviewId { get; set; }
}