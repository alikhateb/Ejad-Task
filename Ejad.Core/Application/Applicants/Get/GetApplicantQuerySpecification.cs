using Ardalis.Specification;
using Ejad.Domain.Ids;
using Ejad.Domain.Models;

namespace Ejad.Core.Application.Applicants.Get;

public sealed class GetApplicantQuerySpecification : Specification<Applicant>
{
    public GetApplicantQuerySpecification(ApplicantId id)
    {
        Query.AsNoTrackingWithIdentityResolution();
        Query.Where(x => x.Id == id);
        Query.Include(x => x.WorkExperiences);
    }
}