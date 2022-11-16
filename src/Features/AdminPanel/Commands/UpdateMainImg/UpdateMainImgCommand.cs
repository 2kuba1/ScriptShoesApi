using MediatR;

namespace ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateMainImg;

public class UpdateMainImgCommand : IRequest
{
    public string ShoeName { get; set; }
    public IFormFile File { get; set; }
}