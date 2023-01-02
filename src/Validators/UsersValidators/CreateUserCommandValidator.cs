using System.Text.RegularExpressions;
using FluentValidation;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Features.Users.Commands.CreateUser;
using ScriptShoesCQRS.Database;

namespace ScriptShoesAPI.Validators.UsersValidators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(AppDbContext dbContext)
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .Custom((value, context) =>
            {
                var searchForEmail = dbContext.Users.FirstOrDefault(u => u.Email == value);

                if (searchForEmail is not null)
                {
                    context.AddFailure("This email already exist");
                }
            });

        RuleFor(x => x.Surname)
            .MaximumLength(60)
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(60)
            .NotEmpty();

        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MaximumLength(25)
            .Must(HasValidPassword)
            .WithMessage("Password must contain special symbol, capital letter and digit");

        RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);
            
        RuleFor(x => x.Username)
            .MaximumLength(25)
            .MinimumLength(3)
            .Custom((val, context) =>
            {
                var searchForUsername = dbContext.Users.FirstOrDefault(u => u.Username == val);
                if (searchForUsername is not null)
                {
                    context.AddFailure("This username is already taken");
                }
            });
    }
    
    private bool HasValidPassword(string pw)
    {
        var lowercase = new Regex("[a-z]+");
        var uppercase = new Regex("[A-Z]+");
        var digit = new Regex("(\\d)+");
        var symbol = new Regex("(\\W)+");

        return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw) && symbol.IsMatch(pw));
    }
}