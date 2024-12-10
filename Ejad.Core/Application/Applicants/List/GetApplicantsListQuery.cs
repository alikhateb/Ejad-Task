using MediatR;

namespace Ejad.Core.Application.Applicants.List;

public class GetApplicantsListQuery : IRequest<List<GetApplicantsListQueryResult>>;