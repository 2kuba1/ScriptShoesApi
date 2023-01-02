using FluentValidation;
using ScriptShoesAPI.Features.Users.Queries.Login;

namespace ScriptShoesAPI.Validators.UsersValidators;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MaximumLength(25)
            .NotEmpty()
            .WithMessage("Password must contain special symbol, capital letter and digit");

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}