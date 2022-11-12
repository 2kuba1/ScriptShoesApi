using MediatR;

namespace ScriptShoesCQRS.Features.Users.Commands.VerifyEmail;

public class VerifyEmailCode : IRequest
{
    public string Code { get; set; }   
}