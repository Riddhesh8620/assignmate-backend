using AssignmateFunctional.Common.DTO;
using FluentValidation;

namespace AssignmateFunctional.Common.Validators;

public sealed class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name is mandatory")
            .MaximumLength(200)
            .WithMessage("Name must not exceed 200 characters");

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        RuleFor(x => x.Phone)
    .NotEmpty().WithMessage("Phone is required.")
    .Matches(@"^\+?[\d\s\-]{10,15}$").WithMessage("Phone must be a valid number (10–15 digits).");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}
