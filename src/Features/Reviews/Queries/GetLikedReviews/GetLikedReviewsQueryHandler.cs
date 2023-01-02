using MediatR;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Reviews.Queries.GetLikedReviews;

public class GetLikedReviewsQueryHandler : IRequestHandler<GetLikedReviewsQuery, IEnumerable<int>>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public GetLikedReviewsQueryHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<IEnumerable<int>> Handle(GetLikedReviewsQuery request, CancellationToken cancellationToken)
    {
        var searchForLikedReviews = _dbContext.ReviewsLikes
            .Where(u => u.UserId == _contextService.GetUserId.Value && u.ShoesId == request.ShoeId).Select(r => r.ReviewId)
            .ToList();

        return searchForLikedReviews;
    }
}