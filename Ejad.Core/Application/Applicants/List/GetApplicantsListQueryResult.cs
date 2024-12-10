using Ejad.Domain.Ids;

namespace Ejad.Core.Application.Applicants.List;

public class GetApplicantsListQueryResult
{
    public ApplicantId Id { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Mobile { get; init; }
    public string PersonalImage { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public string MaritalStatus { get; init; }
    public string LinkedInUrl { get; init; }
    public string JobTitle { get; init; }
    public decimal ExpectedSalary { get; init; }
}