using MediatR;

namespace ScriptShoesAPI.Features.AdminPanel.Commands.UpdateMainImg;

public record UpdateMainImgCommand : IRequest
{
    public string ShoeName { get; set; }
    public IFormFile File { get; set; }
}