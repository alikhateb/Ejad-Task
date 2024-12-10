using Ardalis.Specification;
using Ejad.Domain.Ids;
using Ejad.Domain.Models;

namespace Ejad.Core.Application.Applicants.Details;

internal sealed class GetApplicantDetailsQuerySpecification : Specification<Applicant, GetApplicantDetailsQueryResult>
{
    public GetApplicantDetailsQuerySpecification(ApplicantId id)
    {
        Query.AsNoTrackingWithIdentityResolution();
        Query.Where(x => x.Id == id);
        Query.Include(x => x.WorkExperiences);
        Query.Select(x => new GetApplicantDetailsQueryResult
        {
            Id = x.Id,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            ExpectedSalary = x.ExpectedSalary,
            FullName = x.FullName,
            JobTitle = x.JobTitle,
            LinkedInUrl = x.LinkedInUrl,
            MaritalStatus = x.MaritalStatus.ToString(),
            Mobile = x.Mobile,
            WorkExperiences = x.WorkExperiences.ToList(),
            PersonalImage = x.PersonalImage
        });
    }
}