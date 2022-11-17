using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public DeleteReviewCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var userReviews = _dbContext.Reviews
            .Where(r => r.UserId == _contextService.GetUserId.Value && r.ShoesId == request.ShoeId).ToList();
        if (userReviews.Count == 0)
        {
            throw new PermissionDeniedException("You can't do this");
        }

        var searchForReview =
            await _dbContext.Reviews.FirstOrDefaultAsync(u =>
                u.UserId == _contextService.GetUserId.Value && u.Id == request.ReviewId && u.ShoesId == request.ShoeId, cancellationToken: cancellationToken);
        if (searchForReview is null)
        {
            throw new PermissionDeniedException("You can't do this");
        }

        var shoes = await _dbContext.Shoes.FirstOrDefaultAsync(s => s.Id == request.ShoeId, cancellationToken: cancellationToken);
        shoes.ReviewsNum--;
        _dbContext.Reviews.Remove(searchForReview);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}