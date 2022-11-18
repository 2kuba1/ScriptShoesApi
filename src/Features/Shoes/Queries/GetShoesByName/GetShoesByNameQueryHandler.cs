using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetShoesByName;

public class GetShoesByNameQueryHandler : IRequestHandler<GetShoesByNameQuery, IEnumerable<GetShoesByNameDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetShoesByNameQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetShoesByNameDto>> Handle(GetShoesByNameQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _dbContext.Shoes.Where(s => request.SearchPhrase == null || (
                s.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                s.ShoeType.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                s.Brand.ToLower().Contains(request.SearchPhrase.ToLower())))
            .Include(s => s.MainImages)
            .Include(s => s.Sizes);
        
        var results = _mapper.Map<List<GetShoesByNameDto>>(baseQuery);

        return results;
    }
}