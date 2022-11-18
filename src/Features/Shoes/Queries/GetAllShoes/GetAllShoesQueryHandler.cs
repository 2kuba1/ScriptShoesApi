using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Models.Shoes;

namespace ScriptShoesCQRS.Features.Shoes.Queries.GetAllShoes;

public class GetAllShoesQueryHandler : IRequestHandler<GetAllShoesQuery, IEnumerable<GetAllShoesDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllShoesQueryHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetAllShoesDto>> Handle(GetAllShoesQuery request, CancellationToken cancellationToken)
    {
        var shoesList = _dbContext.Shoes
            .Include(s => s.Reviews)
            .Include(s => s.Sizes)
            .Include(s => s.MainImages)
            .ToList();

        var shoes = _mapper.Map<List<GetAllShoesDto>>(shoesList);

        return shoes;
    }
}