using MediatR;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Features.Users.Queries.RefreshToken;

public class RefreshTokenQuery : IRequest<AuthenticationUserResponse>
{
    public string RefreshToken { get; set; }
}