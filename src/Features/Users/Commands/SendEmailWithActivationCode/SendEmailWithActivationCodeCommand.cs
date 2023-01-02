using MediatR;

namespace ScriptShoesAPI.Features.Users.Commands.SendEmailWithActivationCode;

public record SendEmailWithActivationCodeCommand : IRequest
{
    public string Subject { get; set; }
}