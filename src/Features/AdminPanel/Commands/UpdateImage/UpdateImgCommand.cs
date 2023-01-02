using MediatR;

namespace ScriptShoesAPI.Features.AdminPanel.Commands.UpdateImage;

public record UpdateImgCommand : IRequest
{
    public string ShoeName { get; set; }
    public IFormFile File { get; set; }
}