using FluentValidation;
using KoronaWirusMonitor3.Models;

namespace KWMonitor.Validators
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull().MinimumLength(3).MaximumLength(50);
        }
    }
}