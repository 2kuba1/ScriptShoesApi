using MediatR;

namespace ScriptShoesCQRS.Features.Users.Commands.VerifyEmail;

public record VerifyEmailCode : IRequest
{
    public string Code { get; set; }   
}