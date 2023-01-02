using FluentValidation;
using ScriptShoesCQRS.Models.Reviews;

namespace ScriptShoesAPI.Validators.ReviewsValidators;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(r => r.Title)
            .MinimumLength(3)
            .MaximumLength(25);
        
        RuleFor(r => r.Review)
            .MinimumLength(3)
            .MaximumLength(250);

        RuleFor(r => r.Rate)
            .Custom((value, context) =>
            {
                if (value is < 1 or > 5)
                {
                    context.AddFailure("Rate must be in 1-5 range");
                }
            });
    }
}