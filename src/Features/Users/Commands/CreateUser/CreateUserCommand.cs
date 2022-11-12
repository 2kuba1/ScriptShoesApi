using MediatR;
using ScriptShoesCQRS.Models.Users;

namespace ScriptShoesCQRS.Features.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}