using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Favourites.Commands.RemoveShoeFromFavourites;

public class RemoveShoeFromFavouritesCommandHandler : IRequestHandler<RemoveShoeFromFavouritesCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public RemoveShoeFromFavouritesCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(RemoveShoeFromFavouritesCommand request, CancellationToken cancellationToken)
    {
        var userFavourites = _dbContext.Favorites
            .Where(r => r.UserId == _contextService.GetUserId.Value && r.ShoesId == request.ShoeId).ToList();
        
        if (userFavourites.Count == 0)
        {
            throw new NotFoundException("You don't have any favourites");
        }

        var searchForFavourites =
            await _dbContext.Favorites.FirstOrDefaultAsync(u =>
                u.UserId == _contextService.GetUserId.Value && u.ShoesId == request.ShoeId, cancellationToken: cancellationToken);
        
        if (searchForFavourites is null)
        {
            throw new NotFoundException("Favourite not found");
        }

        _dbContext.Favorites.Remove(searchForFavourites);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}