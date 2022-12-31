using MediatR;
using ScriptShoesApi.Entities;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.Favourites.Commands.AddToFavourites;

public class AddToFavouritesCommandHandler : IRequestHandler<AddToFavouritesCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public AddToFavouritesCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(AddToFavouritesCommand request, CancellationToken cancellationToken)
    {
        var checkIfUserAlreadyHasShoeInFavourites = _dbContext.Favorites.FirstOrDefault(s =>
            s.ShoesId == request.ShoeId && s.UserId == _contextService.GetUserId.Value);

        if (checkIfUserAlreadyHasShoeInFavourites is not null)
        {
            throw new ConflictException("You already have this shoe in favourites");
        }
        
        var favorites = new Favorites()
        {
            ShoesId = request.ShoeId,
            UserId = _contextService.GetUserId.Value
        };

        await _dbContext.Favorites.AddAsync(favorites, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}