using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.Cart.Commands.AddToCart;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public AddToCartCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var searchForShoe = await _dbContext.Cart.FirstOrDefaultAsync(s => s.ShoesId == request.ShoeId, cancellationToken: cancellationToken);
        
        if (searchForShoe is not null)
        {
            throw new ConflictException($"This shoe is already in cart");
        }

        var cart = new Database.Entities.Cart()
        {
            ShoesId = request.ShoeId,
            UserId = _contextService.GetUserId.Value
        };
        
        await _dbContext.Cart.AddAsync(cart, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}