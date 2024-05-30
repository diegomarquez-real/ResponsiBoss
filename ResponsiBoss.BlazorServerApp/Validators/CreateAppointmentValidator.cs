using FluentValidation;
using ResponsiBoss.BlazorServerApp.Pages.Appointment;

namespace ResponsiBoss.BlazorServerApp.Validators
{
    public class CreateAppointmentValidator : AbstractValidator<_CreateAppointment.InputModel>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, properyName) =>
        {
            var result = await ValidateAsync(ValidationContext<_CreateAppointment.InputModel>.CreateWithOptions((_CreateAppointment.InputModel)model, x => x.IncludeProperties(properyName)));

            if (result.IsValid)
                return Array.Empty<string>();

            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}