using Ardalis.Specification;
using Ejad.Domain.Models;

namespace Ejad.Core.Application.Applicants.List;

internal sealed class GetApplicantsListQuerySpecification : Specification<Applicant, GetApplicantsListQueryResult>
{
    public GetApplicantsListQuerySpecification()
    {
        Query.AsNoTrackingWithIdentityResolution();
        Query.Select(x => new GetApplicantsListQueryResult
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
            PersonalImage = x.PersonalImage
        });
    }
}