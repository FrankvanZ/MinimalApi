using Domain.Models;
using FluentValidation;

namespace Api.Validators;

public class PlaceholderValidator : AbstractValidator<Placeholder>
{
    public PlaceholderValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}