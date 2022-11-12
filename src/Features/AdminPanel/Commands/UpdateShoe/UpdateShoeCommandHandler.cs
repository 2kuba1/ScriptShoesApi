using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Entities;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.DiscordLogger;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateShoe;

public class UpdateShoeCommandHandler : IRequestHandler<UpdateShoeCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IDiscordLoggerService _logger;
    private readonly IUserContextService _contextService;

    public UpdateShoeCommandHandler(AppDbContext dbContext, IDiscordLoggerService logger, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(UpdateShoeCommand request, CancellationToken cancellationToken)
    {
        var shoe = await _dbContext.Shoes.FirstOrDefaultAsync(s => request.ShoeName == null || (
            s.Name.ToLower().Contains(request.ShoeName.ToLower())), cancellationToken: cancellationToken);

        if (shoe is null)
        {
            await _logger.LogInformation("Update information",
                $"Admin: {_contextService.User.Claims.FirstOrDefault(c => c.Type == "Username")} has tried to update shoe which does not exist: {request.ShoeName}",
                DiscordLoggerColors.DarkAqua);
            throw new NotFoundException($"Shoe not {request.ShoeName} found");
        }

        var sizesList = _dbContext.ShoeSizes.Where(s => s.ShoesId == shoe.Id).ToList();
        _dbContext.ShoeSizes.RemoveRange(sizesList);

        var splitSizes = request.SizesList.Split(",").ToList();

        foreach (var size in splitSizes.Select(sizes => new ShoeSizes()
                 {
                     ShoesId = shoe.Id,
                     Sizes = sizes
                 }))
        {
            await _dbContext.ShoeSizes.AddAsync(size, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        
        shoe.Name = request.NewName;
        shoe.PreviousPrice = request.PreviousPrice;
        shoe.CurrentPrice = request.CurrentPrice;
        shoe.Brand = request.Brand;
        shoe.ShoeType = request.ShoeType;
        shoe.SizesList = request.SizesList;

        await _logger.LogInformation("Update information",
            $"Admin: {_contextService.User.Claims.FirstOrDefault(c => c.Type == "Username")} has updated shoe: {request.NewName}",
            DiscordLoggerColors.Navy);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}