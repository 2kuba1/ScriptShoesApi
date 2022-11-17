using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.DiscordLogger;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ScriptShoesApi.Entities.Reviews>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;
    private readonly IDiscordLoggerService _logger;

    public CreateReviewCommandHandler(AppDbContext dbContext, IUserContextService contextService, IDiscordLoggerService logger)
    {
        _dbContext = dbContext;
        _contextService = contextService;
        _logger = logger;
    }
    
    public async Task<ScriptShoesApi.Entities.Reviews> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var shoe = await _dbContext.Shoes.FirstOrDefaultAsync(s => s.Id == request.ShoeId, cancellationToken: cancellationToken);

        if (shoe is null)
        {
            throw new NotFoundException($"Shoe with id {request.ShoeId} not found");
        }

        var newReview = new ScriptShoesApi.Entities.Reviews
        {
            Rate = request.Rate,
            Review = request.Review,
            Title = request.Title,
            Username = _contextService.User.FindFirst(c => c.Type == "Username").Value,
            ShoesId = request.ShoeId,
            UserId = _contextService.GetUserId.Value
        };

        await _dbContext.Reviews.AddAsync(newReview, cancellationToken);
        shoe.ReviewsNum++;
        await _logger.LogInformation($"Review alert", $"User {newReview.Username} added new review", DiscordLoggerColors.DarkerGrey);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var shoeRates = _dbContext.Reviews
            .Where(s => s.ShoesId == request.ShoeId)
            .Select(r => r.Rate).ToList();

        var avg = 0;
        for (int i = 0; i < shoeRates.Count; i++)
        {
            avg += shoeRates[i];
        }

        var avgRate = avg / shoeRates.Count;
        shoe.AverageRating = avgRate;
        var currentReview = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == newReview.Id, cancellationToken: cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return currentReview;
    }
}