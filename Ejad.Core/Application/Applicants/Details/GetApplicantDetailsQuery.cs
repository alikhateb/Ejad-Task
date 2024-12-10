using Ejad.Domain.Ids;
using MediatR;

namespace Ejad.Core.Application.Applicants.Details;

public class GetApplicantDetailsQuery(ApplicantId id) : IRequest<GetApplicantDetailsQueryResult>
{
    public ApplicantId Id { get; } = id;
}