using Ejad.Domain.Ids;

namespace Ejad.Domain.Models;

public class Applicant
{
    private readonly List<WorkExperience> _workExperiences = [];

    public Applicant(string fullName, string email, string mobile, DateOnly dateOfBirth,
        MaritalStatus maritalStatus, string linkedInUrl, string jobTitle, decimal expectedSalary)
    {
        Id = ApplicantId.New();
        FullName = fullName;
        Email = email;
        Mobile = mobile;
        DateOfBirth = dateOfBirth;
        MaritalStatus = maritalStatus;
        LinkedInUrl = linkedInUrl;
        JobTitle = jobTitle;
        ExpectedSalary = expectedSalary;
    }

    private Applicant()
    {
    }

    public ApplicantId Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string PersonalImage { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string LinkedInUrl { get; set; }
    public string JobTitle { get; set; }
    public decimal ExpectedSalary { get; set; }
    public IReadOnlyCollection<WorkExperience> WorkExperiences => _workExperiences;

    public void AddWorkExperience(List<WorkExperience> experiences)
    {
        if (experiences is null)
        {
            throw new NullReferenceException(message: "invalid Work Experiences");
        }

        _workExperiences.AddRange(experiences);
    }
}