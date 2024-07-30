using FluentValidation;
using HealthPadiWebApi.DTOs;

namespace HealthPadiBackend.Validators
{
    public class ReportValidation : AbstractValidator<AddReportDto>
    {
        public ReportValidation()
        {
            RuleFor(x => x.Content)
               .NotEmpty()
               .WithMessage("Content must not be empty")
               .MaximumLength(1000);
        }
    }
}
