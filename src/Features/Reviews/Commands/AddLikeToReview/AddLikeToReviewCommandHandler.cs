using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Database.Entities;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.UserContext;
using ScriptShoesCQRS.Database.Entities;

namespace ScriptShoesAPI.Features.Reviews.Commands.AddLikeToReview;

public class AddLikeToReviewCommandHandler : IRequestHandler<AddLikeToReviewCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public AddLikeToReviewCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(AddLikeToReviewCommand request, CancellationToken cancellationToken)
    {
        var searchForLikes = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.ShoesId == request.ShoeId && r.Id == request.ReviewId, cancellationToken: cancellationToken);
        if (searchForLikes is null)
        {
            throw new NotFoundException($"Shoe with id {request.ShoeId} not found");
        }

        var searchForUserLikes =
            await _dbContext.ReviewsLikes.FirstOrDefaultAsync(s =>
                s.UserId == _contextService.GetUserId.Value && s.ReviewId == request.ReviewId, cancellationToken: cancellationToken);

        if (searchForUserLikes is not null)
        {
            throw new ConflictException("You have already liked this review");
        }

        searchForLikes.ReviewLikes++;
        await _dbContext.ReviewsLikes.AddAsync(new ReviewsLikes()
        {
            ShoesId = request.ShoeId,
            ReviewId = request.ReviewId,
            UserId = _contextService.GetUserId.Value
        }, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}