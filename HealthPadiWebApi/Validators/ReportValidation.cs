using FluentValidation;
using HealthPadiWebApi.DTOs.Request;

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

            RuleFor(x => x.Location)
               .NotEmpty();
        }
    }
}
