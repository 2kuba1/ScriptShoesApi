using MediatR;

namespace ScriptShoesCQRS.Features.Users.Commands.SendEmailWithActivationCode;

public record SendEmailWithActivationCodeCommand : IRequest
{
    public string Subject { get; set; }
}