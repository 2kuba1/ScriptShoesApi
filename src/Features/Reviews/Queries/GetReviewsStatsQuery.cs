using MediatR;
using ScriptShoesCQRS.Models.Reviews;

namespace ScriptShoesCQRS.Features.Reviews.Queries;

public class GetReviewsStatsQuery : IRequest<ReviewsStatsDto>
{
    public int ShoeId { get; set; }
}