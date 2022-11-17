using MediatR;

namespace ScriptShoesCQRS.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommand : IRequest<ScriptShoesApi.Entities.Reviews>
{
    public int ShoeId { get; set; }
    public string Title { get; set; }
    public string Review { get; set; }
    public int Rate { get; set; }
}