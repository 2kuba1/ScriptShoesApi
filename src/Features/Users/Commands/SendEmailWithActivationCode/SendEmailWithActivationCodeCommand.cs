using MediatR;

namespace ScriptShoesCQRS.Features.Users.Commands.SendEmailWithActivationCode;

public class SendEmailWithActivationCodeCommand : IRequest
{
    public string Subject { get; set; }
}