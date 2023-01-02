using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.DiscordLogger;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.AdminPanel.Commands.UpdateMainImg;

public class UpdateMainImgCommandHandler : IRequestHandler<UpdateMainImgCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;
    private readonly IDiscordLoggerService _logger;

    public UpdateMainImgCommandHandler(AppDbContext dbContext, IUserContextService contextService,
        IDiscordLoggerService logger)
    {
        _dbContext = dbContext;
        _contextService = contextService;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateMainImgCommand request, CancellationToken cancellationToken)
    {
        var shoe = await _dbContext.Shoes.FirstOrDefaultAsync(s => request.ShoeName == null || (
            s.Name.ToLower().Contains(request.ShoeName.ToLower())), cancellationToken: cancellationToken);

        if (shoe is null)
        {
            throw new NotFoundException("Shoe not found");
        }

        var mainImage =
            await _dbContext.MainImages.FirstOrDefaultAsync(i => i.ShoesId == shoe.Id,
                cancellationToken: cancellationToken);

        var rootPath = Directory.GetCurrentDirectory();
        var fullMainImgPath = $"{rootPath}/wwwroot/MainImages/";

        if (mainImage is null)
        {
            throw new NotFoundException("This shoe does not have main image");
        }

        var pathWithMainImgFile = fullMainImgPath + mainImage.ImageName;
        
        if (!File.Exists(pathWithMainImgFile)) return Unit.Value;

        File.Delete(pathWithMainImgFile);
        
        var newFileName = request.File.FileName.Replace(' ', '_');

        var fullPath = $"{rootPath}/wwwroot/MainImages/{newFileName}";

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        mainImage.ImageName = request.File.FileName;
        mainImage.MainImg = fullPath;

        await _logger.LogInformation("Update information",
            $"Admin: {_contextService.User.Claims.FirstOrDefault(c => c.Type == "Username")} has updated shoe main img: {request.ShoeName}",
            DiscordLoggerColors.Navy);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}