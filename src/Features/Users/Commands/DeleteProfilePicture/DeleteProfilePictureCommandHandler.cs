using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Users.Commands.DeleteProfilePicture;

public class DeleteProfilePictureCommandHandler : IRequestHandler<DeleteProfilePictureCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public DeleteProfilePictureCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }

    public async Task<Unit> Handle(DeleteProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(p =>
            p.Id == _contextService.GetUserId.Value, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var rootPath = Directory.GetCurrentDirectory();
        var fullPath = $"{rootPath}/wwwroot/UsersProfilePictures/";
        var fileName = user.ProfilePictureUrl.Split(fullPath);

        if (fileName.Length != 2)
        {
            return Unit.Value;
        }
        
        var pathWithFile = fullPath + fileName[1];
        
        if (!File.Exists(pathWithFile)) return Unit.Value;
        
        File.Delete(pathWithFile);
        user.ProfilePictureUrl =
            "https://cdn.discordapp.com/attachments/1023999045536059404/1039613165207568404/defaultAvatar.png";
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}