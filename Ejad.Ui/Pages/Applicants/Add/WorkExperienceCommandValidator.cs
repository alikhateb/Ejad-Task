using FluentValidation;

namespace Ejad.Ui.Pages.Applicants.Add;

public class WorkExperienceCommandValidator : AbstractValidator<WorkExperienceCommand>
{
    public WorkExperienceCommandValidator()
    {
        RuleFor(x => x.JobTitle)
            .NotEmpty()
            .WithMessage("JobTitle is required");

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("CompanyName is required");

        RuleFor(x => x.Start)
            .NotEmpty()
            .WithMessage("Start is invalid");

        RuleFor(x => x.End)
            .NotEmpty()
            .WithMessage("End is invalid")
            .Must((x, y) => x.Start < y)
            .WithMessage("End must be greater than start");

        RuleFor(x => x.Salary)
            .GreaterThan(0)
            .WithMessage("Salary should be greater than 0");
    }
}