using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Models.Cart;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Cart.Queries.GetItemsFromCart;

public class GetItemsFromCartQueryHandler : IRequestHandler<GetItemsFromCartQuery, IEnumerable<GetItemsFromCartDto>>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;
    private readonly IMapper _mapper;

    public GetItemsFromCartQueryHandler(AppDbContext dbContext, IUserContextService contextService, IMapper mapper)
    {
        _dbContext = dbContext;
        _contextService = contextService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetItemsFromCartDto>> Handle(GetItemsFromCartQuery request, CancellationToken cancellationToken)
    {
        var getItems = _dbContext.Cart.Where(r => r.UserId == _contextService.GetUserId.Value)
            .Select(f => f.ShoesId).ToList();

        var itemsList = new List<ScriptShoesCQRS.Database.Entities.Shoes>();

        for (int i = 0; i < getItems.Count; i++)
        {
            if (getItems.Count == 0)
            {
                throw new NotFoundException("You don't have any items in cart");
            }
            var shoe = await _dbContext.Shoes.
                Include(g => g.MainImages)
                .FirstOrDefaultAsync(s => s.Id == getItems[i], cancellationToken: cancellationToken);

            if (shoe is null)
            {
                throw new NotFoundException($"Shoe not found");
            }
            
            itemsList.Add(shoe);
        }

        var results = _mapper.Map<List<GetItemsFromCartDto>>(itemsList);
        return results;
    }
}