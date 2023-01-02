using MediatR;

namespace ScriptShoesAPI.Features.Users.Commands.VerifyEmail;

public record VerifyEmailCode : IRequest
{
    public string Code { get; set; }   
}