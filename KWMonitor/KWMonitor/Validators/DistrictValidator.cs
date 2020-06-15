using FluentValidation;
using KoronaWirusMonitor3.Models;

namespace KWMonitor.Validators
{
    public class DistrictValidator : AbstractValidator<District>
    {
        public DistrictValidator()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull().MinimumLength(3).MaximumLength(50);
        }
    }
}