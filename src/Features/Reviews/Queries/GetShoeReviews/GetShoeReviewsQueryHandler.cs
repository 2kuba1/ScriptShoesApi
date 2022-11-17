using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Models.Reviews;

namespace ScriptShoesCQRS.Features.Reviews.Queries.GetShoeReviews;

public class GetShoeReviewsQueryHandler : IRequestHandler<GetShoeReviewsQuery, IEnumerable<ReviewsDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetShoeReviewsQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ReviewsDto>> Handle(GetShoeReviewsQuery request, CancellationToken cancellationToken)
    {
        var review = _dbContext.Reviews.Where(r => r.ShoesId == request.ShoeId).Include(u => u.Users).ToList();

        var result = _mapper.Map<List<ReviewsDto>>(review);
        return result;
    }
}