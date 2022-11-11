using MediatR;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Features.Users.Queries.Login;

public class LoginQuery : IRequest<AuthenticationUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}