using Ejad.Domain.Repositories;
using MediatR;

namespace Ejad.Core.Application.Applicants.Details;

internal class GetApplicantDetailsQueryHandler(IApplicantRepository applicantRepository)
    : IRequestHandler<GetApplicantDetailsQuery, GetApplicantDetailsQueryResult>
{
    public async Task<GetApplicantDetailsQueryResult> Handle(GetApplicantDetailsQuery request, CancellationToken cancellationToken)
    {
        var specification = new GetApplicantDetailsQuerySpecification(request.Id);
        var applicant = await applicantRepository.FirstOrDefaultAsync(specification, cancellationToken);
        if (applicant is null)
        {
            throw new ArgumentException(message: "applicant not found");
        }

        return applicant;
    }
}