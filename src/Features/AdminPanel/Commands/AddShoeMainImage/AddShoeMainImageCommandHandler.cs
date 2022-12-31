using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Entities;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;

namespace ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoeMainImage;

public class AddShoeMainImageCommandHandler : IRequestHandler<AddShoeMainImageCommand, Unit>
{
    private readonly AppDbContext _dbContext;

    public AddShoeMainImageCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddShoeMainImageCommand request, CancellationToken cancellationToken)
    {
        var checkId =
            await _dbContext.Shoes.FirstOrDefaultAsync(s => s.Name == request.ShoeName,
                cancellationToken: cancellationToken);
        var newFileName = request.File.FileName.Replace(' ', '_');
        if (checkId is null)
        {
            throw new ConflictException("This shoe does not exist");
        }

        var checkIfHasMainImage =
            await _dbContext.MainImages.FirstOrDefaultAsync(s => s.ShoesId == checkId.Id,
                cancellationToken: cancellationToken);

        if (checkIfHasMainImage is not null)
        {
            throw new ConflictException("This shoe already has main img");
        }

        var rootPath = Directory.GetCurrentDirectory();
        var fullPath = $"{rootPath}/wwwroot/MainImages/{newFileName}";

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        var mainImage = new MainImages()
        {
            MainImg = fullPath,
            ShoesId = checkId.Id,
            ImageName = newFileName
        };

        await _dbContext.AddAsync(mainImage, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}