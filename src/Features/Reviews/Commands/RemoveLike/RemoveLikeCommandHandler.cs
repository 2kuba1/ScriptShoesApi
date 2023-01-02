using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Reviews.Commands.RemoveLike;

public class RemoveLikeCommandHandler : IRequestHandler<RemoveLikeCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public RemoveLikeCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
    {
        var searchForLikes = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.ShoesId == request.ShoeId && r.Id == request.ReviewId, cancellationToken: cancellationToken);
        if (searchForLikes is null)
        {
            throw new ConflictException($"Review isn't liked");
        }
        
        var searchForUserLikes = await _dbContext.ReviewsLikes.FirstOrDefaultAsync(s =>
            s.UserId == _contextService.GetUserId.Value && s.ReviewId == request.ReviewId, cancellationToken: cancellationToken);

        if (searchForUserLikes is null) return Unit.Value;
        
        _dbContext.ReviewsLikes.Remove(searchForUserLikes);
        searchForLikes.ReviewLikes--;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}