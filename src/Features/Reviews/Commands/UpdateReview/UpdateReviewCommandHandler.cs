using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Models.Reviews;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Reviews.Commands.UpdateReview;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewsDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;
    private readonly IMapper _mapper;

    public UpdateReviewCommandHandler(AppDbContext dbContext, IUserContextService contextService, IMapper mapper)
    {
        _dbContext = dbContext;
        _contextService = contextService;
        _mapper = mapper;
    }
    
    public async Task<ReviewsDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
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

        searchForReview.Rate = request.Rate;
        searchForReview.Title = request.Title;
        searchForReview.Review = request.Review;
        await _dbContext.SaveChangesAsync(cancellationToken);

        var currentReview = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == searchForReview.Id, cancellationToken: cancellationToken);
        var newReview = _mapper.Map<ReviewsDto>(currentReview);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return newReview;
    }
}