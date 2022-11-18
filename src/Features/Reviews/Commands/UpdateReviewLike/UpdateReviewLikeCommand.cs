using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Commands.UpdateReviewLike;

public record UpdateReviewLikeCommand : IRequest
{
    public int ShoeId { get; set; }
    public int ReviewId { get; set; }
    public int Likes { get; set; }
}