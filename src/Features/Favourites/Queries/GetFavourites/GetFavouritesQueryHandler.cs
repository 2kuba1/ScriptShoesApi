using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Models.Favourites;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Favourites.Queries.GetFavourites;

public class GetFavouritesQueryHandler : IRequestHandler<GetFavouritesQuery, IEnumerable<GetFavouritesDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;
    private readonly IMapper _mapper;

    public GetFavouritesQueryHandler(AppDbContext dbContext, IUserContextService contextService, IMapper mapper)
    {
        _dbContext = dbContext;
        _contextService = contextService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetFavouritesDto>> Handle(GetFavouritesQuery request, CancellationToken cancellationToken)
    {
        var userFavourites = _dbContext.Favorites.Where(r => r.UserId == _contextService.GetUserId.Value)
            .Select(f => f.ShoesId).ToList();

        var shoesToAdd = new List<ScriptShoesCQRS.Database.Entities.Shoes>();

        for (int i = 0; i < userFavourites.Count; i++)
        {
            if (userFavourites.Count == 0)
            {
                throw new NotFoundException("You don't have any favourites");
            }
            var shoe = await _dbContext.Shoes.
                Include(g => g.MainImages)
                .FirstOrDefaultAsync(s => s.Id == userFavourites[i], cancellationToken: cancellationToken);
                
            shoesToAdd.Add(shoe);
        }

        var results = _mapper.Map<List<GetFavouritesDto>>(shoesToAdd);
        return results;
    }
}