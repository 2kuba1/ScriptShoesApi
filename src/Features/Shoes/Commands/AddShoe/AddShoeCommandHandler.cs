using AutoMapper;
using MediatR;
using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.DiscordLogger;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.Shoes.Commands.AddShoe;

public class AddShoeCommandHandler : IRequestHandler<AddShoeCommand,int>
{
    private readonly AppDbContext _dbContext;
    private readonly IDiscordLoggerService _logger;
    private readonly IMapper _mapper;
    private readonly IUserContextService _contextService;

    public AddShoeCommandHandler(AppDbContext dbContext, IDiscordLoggerService logger, IMapper mapper, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _contextService = contextService;
    }
    
    public async Task<int> Handle(AddShoeCommand request, CancellationToken cancellationToken)
    {
        var shoe = _mapper.Map<Database.Entities.Shoes>(request);
        shoe.CreatedBy = _contextService.GetUserId.Value;

        shoe.Name = shoe.Name.ToLower();

        await _dbContext.AddAsync(shoe, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var sizeList = request.SizesList.Split(",").ToList();

        foreach (var sizes in sizeList)
        {
            var size = new ShoeSizes()
            {
                ShoesId = shoe.Id,
                Sizes = sizes
            };

            await _dbContext.ShoeSizes.AddAsync(size, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }


        await _logger.LogInformation("Created new shoe",
            $"Admin: {_contextService.User.Claims.FirstOrDefault(c => c.Type == "Username")} created new shoe: {shoe.Name}",
            DiscordLoggerColors.Green);
        return shoe.Id;
    }
}