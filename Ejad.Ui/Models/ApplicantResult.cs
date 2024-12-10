namespace Ejad.Ui.Models;

public class ApplicantResult
{
    public Guid Id { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Mobile { get; init; }
    public string PersonalImage { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public string MaritalStatus { get; init; }
    public string LinkedInUrl { get; init; }
    public string JobTitle { get; init; }
    public decimal ExpectedSalary { get; init; }
    public List<WorkExperienceResult> WorkExperiences { get; set; } = [];
}