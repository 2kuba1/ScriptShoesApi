using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Database.Entities;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.Reviews.Commands.UpdateReviewLike;

public class UpdateReviewLikeCommandHandler : IRequestHandler<UpdateReviewLikeCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public UpdateReviewLikeCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(UpdateReviewLikeCommand request, CancellationToken cancellationToken)
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
            _dbContext.ReviewsLikes.Remove(searchForUserLikes);
            searchForLikes.ReviewLikes = request.Likes;
        }
        else
        {
            searchForLikes.ReviewLikes = request.Likes;
            await _dbContext.ReviewsLikes.AddAsync(new ReviewsLikes()
            {
                ShoesId = request.ShoeId,
                ReviewId = request.ReviewId,
                UserId = _contextService.GetUserId.Value
            }, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}