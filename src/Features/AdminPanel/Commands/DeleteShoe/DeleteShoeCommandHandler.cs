using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.DiscordLogger;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.AdminPanel.Commands.DeleteShoe;

public class DeleteShoeCommandHandler : IRequestHandler<DeleteShoeCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IDiscordLoggerService _logger;
    private readonly IUserContextService _contextService;

    public DeleteShoeCommandHandler(AppDbContext dbContext, IDiscordLoggerService logger, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(DeleteShoeCommand request, CancellationToken cancellationToken)
    {
         var shoe = await _dbContext.Shoes.FirstOrDefaultAsync(s => request.ShoeName == null || (
            s.Name.ToLower().Contains(request.ShoeName.ToLower())), cancellationToken: cancellationToken);

        if (shoe is null)
        {
            await _logger.LogInformation("Delete information",
                $"Admin: {_contextService.User.Claims.FirstOrDefault(c => c.Type == "Username")} has tried to delete shoe which does not exist: {request.ShoeName}",
                DiscordLoggerColors.DarkRed);
            throw new NotFoundException("Shoe not found");
        }

        var sizesList = _dbContext.ShoeSizes.Where(s => s.ShoesId == shoe.Id);
        await _logger.LogInformation("Delete information",
            $"Admin: {_contextService.User.Claims.FirstOrDefault(c => c.Type == "Username")} has deleted shoe: {request.ShoeName}",
            DiscordLoggerColors.DarkRed);

        var mainImage = await _dbContext.MainImages.FirstOrDefaultAsync(i => i.ShoesId == shoe.Id, cancellationToken: cancellationToken);

        if (mainImage is not null)
        {
            _dbContext.MainImages.Remove(mainImage);
        }

        var rootPath = Directory.GetCurrentDirectory();
        var fullMainImgPath = $"{rootPath}/wwwroot/MainImages/";
        
        if (mainImage is not null)
        {
            var pathWithMainImgFile = fullMainImgPath + mainImage.ImageName;
            
            if (!File.Exists(pathWithMainImgFile)) return Unit.Value;
        
            File.Delete(pathWithMainImgFile);
        }
        
        var imagesList = _dbContext.Images.Where(i => i.ShoesId == shoe.Id).ToList();

        if (imagesList.Count != 0)
        {
            foreach (var image in imagesList)
            {
                var fullImgPath = $"{rootPath}/wwwroot/Images/"+image.ImgName;

                if (!File.Exists(fullImgPath))
                {
                    break;
                }

                File.Delete(fullImgPath);
                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        _dbContext.Shoes.Remove(shoe);
        _dbContext.ShoeSizes.RemoveRange(sizesList);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}