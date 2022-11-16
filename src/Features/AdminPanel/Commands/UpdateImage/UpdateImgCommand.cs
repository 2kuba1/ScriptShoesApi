using MediatR;

namespace ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateImage;

public class UpdateImgCommand : IRequest
{
    public string ShoeName { get; set; }
    public IFormFile File { get; set; }
}