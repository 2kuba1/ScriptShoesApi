using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Cart.Commands.DeleteItemFromCart;

public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public RemoveItemFromCartCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        var searchForItemInCart =
            await _dbContext.Cart.FirstOrDefaultAsync(s =>
                s.ShoesId == request.ShoeId && s.UserId == _contextService.GetUserId.Value, cancellationToken: cancellationToken);

        if (searchForItemInCart is null)
        {
            throw new NotFoundException($"Shoe with id {request.ShoeId} not found");
        }

        _dbContext.Cart.Remove(searchForItemInCart);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}