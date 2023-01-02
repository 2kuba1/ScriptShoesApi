using MediatR;
using ScriptShoesAPI.Models.Users;

namespace ScriptShoesAPI.Features.Users.Queries.RefreshToken;

public record RefreshTokenQuery : IRequest<AuthenticationUserResponse>
{
    public string RefreshToken { get; set; }
}