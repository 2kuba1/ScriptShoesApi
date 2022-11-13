using MediatR;

namespace ScriptShoesCQRS.Features.Users.Commands.AddProfilePicture;

public class AddProfilePictureCommand : IRequest
{
    public IFormFile File { get; set; }
}