using MediatR;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Features.Users.Queries.RefreshToken;

public record RefreshTokenQuery : IRequest<AuthenticationUserResponse>
{
    public string RefreshToken { get; set; }
}