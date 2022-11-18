using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Models.Reviews;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetShoeWithContent;

public class GetShoeWithContentQueryHandler : IRequestHandler<GetShoeWithContentQuery, GetShoeWithContentResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetShoeWithContentQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<GetShoeWithContentResponse> Handle(GetShoeWithContentQuery request, CancellationToken cancellationToken)
    {
        var shoe = await _dbContext.Shoes
            .Include(s => s.Images)
            .Include(s => s.Sizes)
            .Include(s => s.Reviews)
            .Include(s => s.MainImages)
            .FirstOrDefaultAsync(s => s.Name == request.ShoeName, cancellationToken: cancellationToken);

        if (shoe is null)
        {
            throw new NotFoundException($"Shoe {request.ShoeName} not found");
        }

        var result = _mapper.Map<GetShoeWithContentDto>(shoe);
        
        var shoesAndReviews = new GetShoeWithContentResponse()
        {
            Reviews = GetShoeReviews(shoe.Id),
            Shoes = result
        };
        
        return shoesAndReviews;
    }
    private IEnumerable<ReviewsDto> GetShoeReviews(int shoeId)
    {
        var review = _dbContext.Reviews.Where(r => r.ShoesId == shoeId).Include(u => u.Users).ToList();
        
        var result = _mapper.Map<List<ReviewsDto>>(review);
        return result;
    }
}