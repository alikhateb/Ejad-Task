using System.Text.RegularExpressions;
using FluentValidation;

namespace Ejad.Ui.Pages.Applicants.Add;

public partial class CreateApplicantCommandValidator : AbstractValidator<CreateApplicantCommand>
{
    public CreateApplicantCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("FullName is required");

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
    }

    [GeneratedRegex("^[+]?[0-9]+$")]
    private static partial Regex MobileRegex();

    [GeneratedRegex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
    private static partial Regex EmailRegex();
}