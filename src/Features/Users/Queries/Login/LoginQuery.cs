using MediatR;
using ScriptShoesAPI.Models.Users;

namespace ScriptShoesAPI.Features.Users.Queries.Login;

public record LoginQuery : IRequest<AuthenticationUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}