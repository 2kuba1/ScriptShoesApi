using MediatR;

namespace ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateImage;

public record UpdateImgCommand : IRequest
{
    public string ShoeName { get; set; }
    public IFormFile File { get; set; }
}