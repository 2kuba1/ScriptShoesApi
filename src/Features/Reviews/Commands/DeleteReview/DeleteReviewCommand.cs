using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Commands.DeleteReview;

public class DeleteReviewCommand : IRequest
{
    public int ShoeId { get; set; }
    public int ReviewId { get; set; }
}