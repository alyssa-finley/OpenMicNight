using System;
using System.Data;
using FluentValidation;
using OpenMicNight.Data;
using OpenMicNight.Domain;

namespace OpenMicNight.Validators
{
    public class PerformanceValidator : AbstractValidator<Performance>
    {
        public PerformanceValidator()
        {
            var validPerformerTypes = new List<string>() { "Music", "Comedy", "Poetry" };
            RuleFor(performance => performance.PerformerType)
                .Must(x => validPerformerTypes.Contains(x))
                .WithMessage("Please only use: " + String.Join(",", validPerformerTypes));
            RuleFor(performance => performance.PerformerName).NotEmpty();
        }
    }
}
