using Ejad.Domain.Ids;
using Ejad.Domain.Models;

namespace Ejad.Core.Application.Applicants.Get;

public class GetApplicantQueryResult
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
    public List<WorkExperience> WorkExperiences { get; init; }
}