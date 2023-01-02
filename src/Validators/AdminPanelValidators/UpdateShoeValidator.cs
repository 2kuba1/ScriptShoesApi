using FluentValidation;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateShoe;

namespace ScriptShoesAPI.Validators.AdminPanelValidators;

public class UpdateShoeValidator : AbstractValidator<UpdateShoeCommand>
{
    public UpdateShoeValidator(AppDbContext dbContext)
    {
        RuleFor(x => x.Brand)
            .NotEmpty();

        RuleFor(x => x.NewName)
            .NotEmpty()
            .Custom((val, context) =>
            {
                var searchForName = dbContext.Shoes.FirstOrDefault(s => s.Name == val);
                if (searchForName is not null)
                {
                    context.AddFailure("Shoe with this name already exist");
                }
            });

        RuleFor(x => x.CurrentPrice)
            .NotEmpty();
    }
}