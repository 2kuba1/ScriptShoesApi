using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Database.Entities;
using ScriptShoesApi.Exceptions;

namespace ScriptShoesAPI.Features.AdminPanel.Commands.AddShoeImage;

public class AddShoeImageCommandHandler : IRequestHandler<AddShoeImageCommand, Unit>
{
    private readonly AppDbContext _dbContext;

    public AddShoeImageCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(AddShoeImageCommand request, CancellationToken cancellationToken)
    {
        var checkId =
            await _dbContext.Shoes.FirstOrDefaultAsync(s => s.Name == request.ShoeName,
                cancellationToken: cancellationToken);
        var newFileName = request.File.FileName.Replace(' ', '_');
        if (checkId is null)
        {
            throw new ConflictException("This shoe does not exist");
        }

        var rootPath = Directory.GetCurrentDirectory();
        var fullPath = $"{rootPath}/wwwroot/Images/{newFileName}";

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        var image = new Images()
        {
            Img = fullPath,
            ShoesId = checkId.Id,
            ImgName = newFileName
        };

        await _dbContext.AddAsync(image, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}