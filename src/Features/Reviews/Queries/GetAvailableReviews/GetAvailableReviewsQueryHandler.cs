using MediatR;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Reviews.Queries.GetAvailableReviews;

public class GetAvailableReviewsQueryHandler : IRequestHandler<GetAvailableReviewsQuery, IEnumerable<int>>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public GetAvailableReviewsQueryHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<IEnumerable<int>> Handle(GetAvailableReviewsQuery request, CancellationToken cancellationToken)
    {
        var userReviews = _dbContext.Reviews
            .Where(r => r.ShoesId == request.ShoeId && r.UserId == _contextService.GetUserId.Value)
            .Select(i => i.Id)
            .ToList();

        if (userReviews is null)
        {
            throw new NotFoundException("0 reviews found");
        }

        return userReviews;
    }
}