using FluentValidation;
using System.Text.RegularExpressions;

namespace Ejad.Core.Application.Applicants.Create;

public partial class CreateApplicantCommandValidator : AbstractValidator<CreateApplicantCommand>
{
    public CreateApplicantCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("FullName is required 01026360888");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("FullName is required")
            .Matches(EmailRegex())
            .WithMessage("invalid email");

        RuleFor(x => x.Mobile)
            .NotEmpty()
            .WithMessage("Mobile is required")
            .Matches(MobileRegex())
            .WithMessage("mobile is accept numbers only");

        RuleFor(x => x.MaritalStatus)
            .IsInEnum()
            .WithMessage("Marital Status is invalid");

        RuleFor(x => x.LinkedInUrl)
            .NotEmpty()
            .WithMessage("LinkedInUrl is required");

        RuleFor(x => x.JobTitle)
            .NotEmpty()
            .WithMessage("JobTitle is required");

        RuleFor(x => x.ExpectedSalary)
            .GreaterThan(0)
            .WithMessage("ExpectedSalary should be greater than 0");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("DateOfBirth is invalid");

        When(x => x.Experiences.Count != 0, () =>
        {
            RuleForEach(x => x.Experiences)
                .ChildRules(experience =>
                {
                    experience.RuleFor(x => x.JobTitle)
                        .NotEmpty()
                        .WithMessage("JobTitle is required");

                    experience.RuleFor(x => x.CompanyName)
                        .NotEmpty()
                        .WithMessage("CompanyName is required");

                    experience.RuleFor(x => x.Start)
                        .NotEmpty()
                        .WithMessage("Start is invalid");

                    experience.RuleFor(x => x.End)
                        .NotEmpty()
                        .WithMessage("End is invalid")
                        .Must((x, y) => x.Start < y)
                        .WithMessage("End must be greater than start");

                    experience.RuleFor(x => x.Salary)
                        .GreaterThan(0)
                        .WithMessage("Salary should be greater than 0");
                });

        });
    }

    [GeneratedRegex("^[+]?[0-9]+$")]
    private static partial Regex MobileRegex();

    [GeneratedRegex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
    private static partial Regex EmailRegex();
}