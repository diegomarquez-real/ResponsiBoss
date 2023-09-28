using FluentValidation;
using ResponsiBoss.BlazorServerApp.Pages;

namespace ResponsiBoss.BlazorServerApp.Validators
{
    public class RegisterValidator : AbstractValidator<Register.InputModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("A valid email is required."); ;
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (! ? * .).");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .NotNull()
                .Equal(_ => _.Password)
                .WithMessage("Passwords do not match.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, properyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Register.InputModel>.CreateWithOptions((Register.InputModel)model, x => x.IncludeProperties(properyName)));

            if (result.IsValid)
                return Array.Empty<string>();

            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}