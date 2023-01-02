using MediatR;

namespace ScriptShoesAPI.Features.AdminPanel.Commands.AddShoeImage;

public record AddShoeImageCommand : IRequest
{
    public IFormFile File { get; set; }
    public string ShoeName { get; set; }
}