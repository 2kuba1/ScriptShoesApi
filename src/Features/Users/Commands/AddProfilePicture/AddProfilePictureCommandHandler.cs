using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Users.Commands.AddProfilePicture;

public class AddProfilePictureCommandHandler : IRequestHandler<AddProfilePictureCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public AddProfilePictureCommandHandler(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }

    public async Task<Unit> Handle(AddProfilePictureCommand request, CancellationToken cancellationToken)
    {
        if (request.File is null || request.File.Length < 0)
        {
            throw new ConflictException("File is null");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(p =>
            p.Id == _contextService.GetUserId.Value, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var fileExtension = request.File.FileName.Split('.'); //fileExtension[1] - extension
        var newFileName = request.File.FileName.Replace(request.File.FileName, user.Username + "." + fileExtension[1]);
        
        var rootPath = Directory.GetCurrentDirectory();
        var fullPath = $"{rootPath}/wwwroot/UsersProfilePictures/{newFileName}";
        
        if (user.ProfilePictureUrl !=
            "https://cdn.discordapp.com/attachments/1023999045536059404/1039613165207568404/defaultAvatar.png")
        {
            var previousFileName =
                user.ProfilePictureUrl.Split(fullPath); // previousFileName[0] - C:\Users\ADMIN\Desktop\SCRIPTSHOESCQRS\ScriptShoesCQRS\ScriptShoesCQRS\src/wwwroot/UsersProfilePictures/ [1] - fileName

            var filePath = previousFileName[0] + previousFileName[1];
            
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        user.ProfilePictureUrl =
            $"{fullPath}";
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}