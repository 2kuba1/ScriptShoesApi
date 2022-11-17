using MediatR;
using ScriptShoesCQRS.Models.Reviews;

namespace ScriptShoesCQRS.Features.Reviews.Commands.UpdateReview;

public class UpdateReviewCommand : IRequest<ReviewsDto>
{
    public int ShoeId { get; set; }
    public int ReviewId { get; set; }
    
    public string Title { get; set; }
    public string Review { get; set; }
    public int Rate { get; set; }
}