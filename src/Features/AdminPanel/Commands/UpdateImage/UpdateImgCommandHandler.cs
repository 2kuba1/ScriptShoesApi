using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.DiscordLogger;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateImage;

public class UpdateImgCommandHandler : IRequestHandler<UpdateImgCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;
    private readonly IDiscordLoggerService _logger;

    public UpdateImgCommandHandler(AppDbContext dbContext, IUserContextService contextService, IDiscordLoggerService logger)
    {
        _dbContext = dbContext;
        _contextService = contextService;
        _logger = logger;
    }
    
    public async Task<Unit> Handle(UpdateImgCommand request, CancellationToken cancellationToken)
    {
        var shoe = await _dbContext.Shoes.FirstOrDefaultAsync(s => request.ShoeName == null || (
            s.Name.ToLower().Contains(request.ShoeName.ToLower())), cancellationToken: cancellationToken);

        if (shoe is null)
        {
            throw new NotFoundException("Shoe not found");
        }

        var image =
            await _dbContext.Images.FirstOrDefaultAsync(i => i.ShoesId == shoe.Id,
                cancellationToken: cancellationToken);

        var rootPath = Directory.GetCurrentDirectory();
        var fullImgPath = $"{rootPath}/wwwroot/Images/";

        if (image is null)
        {
            throw new NotFoundException("This shoe does not have any image");
        }

        var pathWithImgFile = fullImgPath + image.ImgName;
        
        if (!File.Exists(pathWithImgFile)) return Unit.Value;

        File.Delete(pathWithImgFile);
        
        var newFileName = request.File.FileName.Replace(' ', '_');

        var fullPath = $"{rootPath}/wwwroot/Images/{newFileName}";

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        image.ImgName = request.File.FileName;
        image.Img = fullPath;

        await _logger.LogInformation("Update information",
            $"Admin: {_contextService.User.Claims.FirstOrDefault(c => c.Type == "Username")} has updated shoe img: {request.ShoeName}",
            DiscordLoggerColors.Navy);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}