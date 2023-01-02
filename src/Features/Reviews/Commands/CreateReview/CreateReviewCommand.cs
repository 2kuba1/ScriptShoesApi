using MediatR;

namespace ScriptShoesAPI.Features.Reviews.Commands.CreateReview;

public record CreateReviewCommand : IRequest<Database.Entities.Reviews>
{
    public int ShoeId { get; set; }
    public string Title { get; set; }
    public string Review { get; set; }
    public int Rate { get; set; }
}