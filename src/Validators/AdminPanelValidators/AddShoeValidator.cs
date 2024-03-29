﻿using FluentValidation;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Features.AdminPanel.Commands.AddShoe;
using ScriptShoesCQRS.Database;

namespace ScriptShoesAPI.Validators.AdminPanelValidators;

public class AddShoeValidator : AbstractValidator<AddShoeCommand>
{
    public AddShoeValidator(AppDbContext dbContext)
    {
        RuleFor(x => x.Brand)
            .NotEmpty();

        RuleFor(x => x.Name)
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