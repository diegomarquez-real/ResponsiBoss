using FluentValidation;
using ResponsiBoss.BlazorServerApp.Pages;

namespace ResponsiBoss.BlazorServerApp.Validators
{
    public class LoginValidator : AbstractValidator<Login.InputModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, properyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Login.InputModel>.CreateWithOptions((Login.InputModel)model, x => x.IncludeProperties(properyName)));

            if (result.IsValid)
                return Array.Empty<string>();

            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}