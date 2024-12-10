using Ejad.Domain.Repositories;
using MediatR;

namespace Ejad.Core.Application.Applicants.List;

internal class GetApplicantsListQueryHandler(IApplicantRepository applicantRepository)
    : IRequestHandler<GetApplicantsListQuery, List<GetApplicantsListQueryResult>>
{
    public async Task<List<GetApplicantsListQueryResult>> Handle(GetApplicantsListQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new GetApplicantsListQuerySpecification();
        var applicants = await applicantRepository.ListAsync(specification, cancellationToken);
        return applicants;
    }
}