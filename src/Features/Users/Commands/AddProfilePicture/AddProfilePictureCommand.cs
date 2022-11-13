using MediatR;

namespace ScriptShoesCQRS.Features.Users.Commands.AddProfilePicture;

public record AddProfilePictureCommand : IRequest
{
    public IFormFile File { get; set; }
}