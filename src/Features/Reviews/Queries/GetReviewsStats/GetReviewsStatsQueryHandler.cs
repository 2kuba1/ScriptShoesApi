using MediatR;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Models.Reviews;

namespace ScriptShoesAPI.Features.Reviews.Queries.GetReviewsStats;

public class GetReviewsStatsQueryHandler : IRequestHandler<GetReviewsStatsQuery, ReviewsStatsDto>
{
    private readonly AppDbContext _dbContext;

    public GetReviewsStatsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ReviewsStatsDto> Handle(GetReviewsStatsQuery request, CancellationToken cancellationToken)
    {
        var shoeRates = _dbContext.Reviews
            .Where(s => s.ShoesId == request.ShoeId)
            .Select(r => r.Rate).ToList();

        var avg = 0;
        var oneStars = 0;
        var twoStars = 0;
        var threeStars = 0;
        var fourStars = 0;
        var fiveStars = 0;

        for (int i = 0; i < shoeRates.Count; i++)
        {
            avg += shoeRates[i];

            switch (shoeRates[i])
            {
                case 1:
                    oneStars++;
                    break;
                case 2:
                    twoStars++;
                    break;
                case 3:
                    threeStars++;
                    break;
                case 4:
                    fourStars++;
                    break;
                case 5:
                    fiveStars++;
                    break;
            }
        }

        var avgRate = avg / shoeRates.Count;

        return new ReviewsStatsDto()
        {
            AvgRate = avgRate,
            OneStarCount = oneStars,
            TwoStarCount = twoStars,
            ThreeStarCount = threeStars,
            FourStarCount = fourStars,
            FiveStarCount = fiveStars
        };
    }
}