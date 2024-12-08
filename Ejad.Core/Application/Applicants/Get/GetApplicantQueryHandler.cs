using Ejad.Domain.Repositories;
using MediatR;

namespace Ejad.Core.Application.Applicants.Get;

public class GetApplicantQueryHandler(IApplicantRepository applicantRepository)
    : IRequestHandler<GetApplicantQuery, GetApplicantQueryResult>
{
    public async Task<GetApplicantQueryResult> Handle(GetApplicantQuery request, CancellationToken cancellationToken)
    {
        var specification = new GetApplicantQuerySpecification(request.Id);
        var applicant = await applicantRepository.FirstOrDefaultAsync(specification, cancellationToken);
        if (applicant is null)
        {
            throw new ArgumentException(message: "applicant not found");
        }

        var result = new GetApplicantQueryResult
        {
            Id = applicant.Id,
            Email = applicant.Email,
            DateOfBirth = applicant.DateOfBirth,
            ExpectedSalary = applicant.ExpectedSalary,
            FullName = applicant.FullName,
            JobTitle = applicant.JobTitle,
            LinkedInUrl = applicant.LinkedInUrl,
            MaritalStatus = applicant.MaritalStatus.ToString(),
            Mobile = applicant.Mobile,
            WorkExperiences = applicant.WorkExperiences.ToList(),
            PersonalImage = applicant.PersonalImage
        };

        return result;
    }
}