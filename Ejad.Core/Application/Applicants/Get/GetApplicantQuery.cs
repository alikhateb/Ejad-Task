using Ejad.Domain.Ids;
using MediatR;

namespace Ejad.Core.Application.Applicants.Get;

public class GetApplicantQuery(ApplicantId id) : IRequest<GetApplicantQueryResult>
{
    public ApplicantId Id { get; } = id;
}